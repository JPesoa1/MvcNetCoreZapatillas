using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcNetCoreZapatillas.Data;
using MvcNetCoreZapatillas.Models;

#region SQL SERVER
//VUESTRO PROCEDIMIENTO DE PAGINACION DE IMAGENES DE ZAPATILLAS



//CREATE PROCEDURE SP_ZAPATILLAS
//(@POSICION INT
//)
//AS

//    SELECT * FROM
//        (SELECT CAST(
//            ROW_NUMBER() OVER (ORDER BY IDIMAGEN) AS INT) AS POSICION, 
//          IDIMAGEN, IDPRODUCTO, IMAGEN
//        FROM IMAGENESZAPASPRACTICA
//       ) AS QUERY
//    WHERE QUERY.POSICION >= @POSICION AND QUERY.POSICION < (@POSICION + 1)
//GO
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
            => this.context.Zapatillas.ToList();


        public Zapatilla FindZapatillas(int id)
        {
            return this.context.Zapatillas.FirstOrDefault(x => x.IdProducto == id);

        }

        public List<ImagenZapatilla> GetImagenesZapatillas(int id)
        {
            return this.context.ImagenesZapatillas.Where(x => x.IdProducto == id).ToList();
        }

        public int GetNumeroRegistrosImagenesZapatillas(int id)
        {
            return this.context.ImagenesZapatillas.Where(x => x.IdProducto == id).ToList().Count();
        }

        public async Task<List<Zapatilla>> GetImagenesZapatillasPosicion
            (int posicion)
        {
            string sql =
                "SP_ZAPATILLAS @POSICION ";

        SqlParameter pamposicion =
            new SqlParameter("@POSICION", posicion);


        var consulta =
            this.context.Zapatillas.FromSqlRaw(sql
            , pamposicion);
        List<Zapatilla> zapatillas = await consulta.ToListAsync();

            return zapatillas;
                
        }
}
}
