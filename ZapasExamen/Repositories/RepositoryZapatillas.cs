using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using ZapasExamen.Data;
using ZapasExamen.Models;

namespace ZapasExamen.Repositories
{

    #region
    //    CREATE PROCEDURE SP_IMAGENES_ZAPATILLAS
    //(@IDPRODUCTO INT, @POSICION INT)
    //AS
    //    SELECT IDIMAGEN, IDPRODUCTO, IMAGEN FROM
    //        (SELECT CAST(
    //            ROW_NUMBER() OVER(ORDER BY IDIMAGEN) AS INT) 
    //			AS POSICION,
    //            IDIMAGEN, IDPRODUCTO, IMAGEN

    //        FROM IMAGENESZAPASPRACTICA

    //        WHERE IDPRODUCTO = @IDPRODUCTO) AS QUERY

    //    WHERE QUERY.POSICION = @POSICION
    //GO
    #endregion


    public class RepositoryZapatillas
    {
        private Data.ZapatillaContext context;
        public RepositoryZapatillas(ZapatillaContext context)
        {
            this.context = context;
        }
        public async Task<List<Zapatilla>> GetZapatillasAsync()
        {
            return await this.context.zapa.ToListAsync();
        }
        public async Task<Zapatilla> FindZapatillaAsync(int idproducto)
        {
            return await this.context.zapa.Where(z => z.IdProducto == idproducto).FirstOrDefaultAsync();
        }
        public async Task<ModelPaginacionImagenes> GetPaginacionImagenesAsync(int posicion, int idhospital)
        {
            string sql = "SP_SALA_HOSPITAL @posicion, @idhospital, @numregistros out";
            SqlParameter pamPosicion = new SqlParameter("@posicion", posicion);
            SqlParameter pamIdHospital = new SqlParameter("@idhospital", idhospital);

            var consulta = await this.context.zapa.FromSqlRaw(sql, pamPosicion, pamIdHospital).ToListAsync();

            Zapatilla zapa = consulta.FirstOrDefault();
            return new ModelPaginacionImagenes
            {
                Zapa = zapa,
                ImagenZapatilla = new ImagenZapatilla
                {
                    IdProducto = zapa.IdProducto,
                    Imagen = zapa.Descripcion
                },
                NumRegistros = consulta.Count
            };


        }
    }
}
