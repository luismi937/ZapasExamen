# ZapasExamen - Instrucciones de Configuración

## ?? PASOS PARA ARREGLAR EL ERROR COMPLETAMENTE

### 1. **Configurar la Cadena de Conexión**
Abre el archivo `appsettings.json` y **configura la contraseña de tu SQL Server**:

```json
{
  "ConnectionStrings": {
    "localhost": "Data Source=LOCALHOST\\DEVELOPER;Initial Catalog=ZAPAS;User ID=SA;Password=TU_CONTRASEÑA_AQUI;Trust Server Certificate=True;TrustServerCertificate=True"
  }
}
```

**?? IMPORTANTE**: Reemplaza `TU_CONTRASEÑA_AQUI` con la contraseña real de tu usuario SA.

### 2. **Ejecutar el Script SQL**
1. Abre **SQL Server Management Studio** o **Azure Data Studio**
2. Conéctate a `LOCALHOST\DEVELOPER`
3. Abre el archivo `ZapasExamen/SQL/ScriptCompleto.sql`
4. Ejecuta todo el script (F5)
5. Verifica que veas estos mensajes:
   - ? Tabla ZAPASPRACTICA creada correctamente
   - ? Tabla IMAGENESZAPASPRACTICA creada correctamente
   - ? Datos insertados correctamente
   - ? Stored procedure SP_IMAGENES_ZAPATILLAS creado correctamente

### 3. **Reiniciar la Aplicación**
1. **DETÉN el debugger** completamente (Shift+F5)
2. **Limpia la solución**: Build ? Clean Solution
3. **Reconstruye**: Build ? Rebuild Solution (Ctrl+Shift+B)
4. **Inicia la aplicación**: F5

### 4. **Probar la Aplicación**
1. La aplicación se abrirá en el navegador
2. Haz clic en **"Zapatillas"** en el menú superior
3. Deberías ver el listado de 9 zapatillas
4. Haz clic en cualquier zapatilla para ver sus detalles
5. Las imágenes deben cargarse con paginación (Anterior/Siguiente)

---

## ?? Archivos Modificados

### ? `ZapatillaContext.cs`
- Configurado `.ValueGeneratedNever()` para las claves primarias
- Esto indica a EF Core que las columnas NO son IDENTITY

### ? `ZapatillasController.cs`
- El action `Index` ahora retorna la vista "Zapatilla"
- Toda la lógica de paginación está correcta

### ? `appsettings.json`
- Agregado el parámetro `Password` a la cadena de conexión
- **RECUERDA CONFIGURAR TU CONTRASEÑA**

---

## ?? Explicación del Error

### El Problema
**Error**: "El nombre de objeto 'ZAPASPRACTICA' no es válido"

### La Causa Raíz
La tabla `ZAPASPRACTICA` no existía en la base de datos SQL Server `ZAPAS`.

### La Solución
1. **Crear las tablas** ejecutando el script SQL
2. **Configurar EF Core** para que entienda que las columnas de ID no son auto-incrementales
3. **Configurar correctamente** la cadena de conexión con la contraseña

---

## ?? Arquitectura de la Aplicación

```
ZapasExamen/
??? Controllers/
?   ??? ZapatillasController.cs    ? Maneja las peticiones HTTP
??? Data/
?   ??? ZapatillaContext.cs        ? Contexto de Entity Framework
??? Models/
?   ??? Zapatilla.cs               ? Entidad Zapatilla
?   ??? ImagenZapatilla.cs         ? Entidad Imagen
?   ??? ModelPaginacionImagenes.cs ? ViewModel para paginación
??? Repositories/
?   ??? RepositoryZapatillas.cs    ? Lógica de acceso a datos
??? Views/
?   ??? Zapatillas/
?       ??? Zapatilla.cshtml       ? Lista de zapatillas
?       ??? Detalles.cshtml        ? Detalles de una zapatilla
?       ??? _PaginacionImagenes.cshtml ? Vista parcial de imágenes
??? SQL/
    ??? ScriptCompleto.sql         ? Script de inicialización de BD
```

---

## ? Funcionalidades

1. **Listado de Zapatillas**: Ver todas las zapatillas disponibles
2. **Detalles**: Ver información completa de cada zapatilla
3. **Galería de Imágenes**: Navegar por las imágenes con paginación Ajax
4. **Stored Procedure**: Consulta optimizada para obtener imágenes paginadas

---

## ?? Verificación de Funcionamiento

Ejecuta estas consultas en SQL Server para verificar:

```sql
-- Ver todas las zapatillas
SELECT * FROM ZAPASPRACTICA

-- Ver todas las imágenes
SELECT * FROM IMAGENESZAPASPRACTICA

-- Probar el stored procedure
EXEC SP_IMAGENES_ZAPATILLAS @IDPRODUCTO = 9, @POSICION = 1
```

---

## ? Solución de Problemas

### Si sigue dando error de conexión:
- Verifica que SQL Server esté corriendo
- Verifica el nombre de la instancia: `LOCALHOST\DEVELOPER`
- Verifica que la base de datos `ZAPAS` exista
- Verifica la contraseña del usuario SA

### Si las imágenes no cargan:
- Abre las herramientas de desarrollador del navegador (F12)
- Verifica la consola de JavaScript para errores
- Verifica que jQuery esté cargado

### Si el stored procedure falla:
- Ejecuta manualmente el script de creación del SP
- Verifica que no haya errores de sintaxis

---

## ?? Resumen

**TODO LO QUE NECESITAS HACER:**
1. Configura la contraseña en `appsettings.json`
2. Ejecuta el script SQL `ScriptCompleto.sql`
3. Detén y reinicia la aplicación
4. ¡Disfruta! ??
