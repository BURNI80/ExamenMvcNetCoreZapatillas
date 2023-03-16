using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcNetCoreZapatillas.Data;
using MvcNetCoreZapatillas.Models;

#region SQL SERVER
//CREATE OR ALTER PROCEDURE SP_IMAGENESZAPATILLA
//	(@IDPRODUCTO INT, @POSICION INT)
//	AS
//    BEGIN
//		SELECT *
//		FROM (SELECT CAST(
//    ROW_NUMBER() OVER(ORDER BY IDIMAGEN) AS INT) AS POSICION,
//    ISNULL(IDIMAGEN, 0) AS IDIMAGEN, IDPRODUCTO, IMAGEN
//	FROM IMAGENESZAPASPRACTICA
//	WHERE IDPRODUCTO = @IDPRODUCTO) AS QUERY
//		WHERE POSICION >= @POSICION AND POSICION < (@POSICION + 1)
//	END
#endregion

namespace MvcNetCoreZapatillas.Repositories
{
    public class RepositoryZapatillas
    {
        private ZapatillasContext context;

        public RepositoryZapatillas(ZapatillasContext context)
        {
            this.context = context;
        }

        public List<Zapatilla> GetZapatillas()
        {
            return this.context.Zapatillas.ToList();
        }

        public Zapatilla FindZapatilla(int id)
        {
            return this.context.Zapatillas.Where(x => x.IdProducto == id).FirstOrDefault();
        }

        public List<ImagenZapatilla> GetImagenes(int id)
        {
            return this.context.ImagenesZapatillas.Where(x=> x.IdProducto == id).ToList();
        }

        public int GetTotalImagenes(int id)
        {
            return this.context.ImagenesZapatillas.Where(x=> x.IdProducto == id).Count();
        }

        public ImagenZapatilla GetImagen(int idProducto, int posicion)
        {
            string sql = "SP_IMAGENESZAPATILLA @IDPRODUCTO, @POSICION";
            SqlParameter p1 = new SqlParameter("@IDPRODUCTO", idProducto);
            SqlParameter p2 = new SqlParameter("@POSICION", posicion);
            var consulta = this.context.ImagenesZapatillas.FromSqlRaw(sql, p1, p2);
            return consulta.AsEnumerable().FirstOrDefault();
        }
    }
}
