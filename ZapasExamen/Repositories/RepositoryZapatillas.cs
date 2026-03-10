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
        public async Task<ModelPaginacionImagenes> GetPaginacionImagenesAsync(int posicion, int idproducto)
        {
            SqlParameter pamIdProducto = new SqlParameter("@idproducto", idproducto);
            SqlParameter pamPosicion = new SqlParameter("@posicion", posicion);

            var consulta = await this.context.ImagenesZapatillas
                .FromSqlRaw("SP_IMAGENES_ZAPATILLAS @idproducto, @posicion", pamIdProducto, pamPosicion)
                .ToListAsync();

            ImagenZapatilla imagen = consulta.FirstOrDefault();
            
            if (imagen == null)
            {
                return null;
            }

            var zapa = await this.FindZapatillaAsync(idproducto);
            
            var totalImagenes = await this.context.ImagenesZapatillas
                .Where(i => i.IdProducto == idproducto)
                .CountAsync();

            return new ModelPaginacionImagenes
            {
                Zapa = zapa,
                ImagenZapatilla = imagen,
                NumRegistros = totalImagenes
            };
        }
    }
}
