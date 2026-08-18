# Arquitectura pública

## Principio rector

La lógica de negocio y del agente vive en un único núcleo .NET. Web, desktop y CLI presentan estado y transportan comandos; no reimplementan permisos ni herramientas.

| Capa | Responsabilidad | No conoce |
| --- | --- | --- |
| Domain | Entidades e invariantes | HTTP, EF Core, providers |
| Application | Casos de uso y contratos | Persistencia concreta |
| Infrastructure | EF Core, cifrado, providers y runners | UI |
| Hosts | Transporte, auth y composición | Reglas duplicadas |
| Clientes | Interacción y representación | Secretos |

## Flujo sensible

1. El cliente autentica con JWT y refresh token.
2. Application construye una solicitud neutral al proveedor.
3. Toda herramienta propuesta atraviesa el motor de permisos.
4. Las mutaciones producen evidencia revisable y checkpoints.
5. Infrastructure persiste sin exponer credenciales.

## Atributos de calidad

- Seguridad por defecto y denegaciones explícitas.
- Portabilidad entre PostgreSQL y SQLite.
- Proveedores reemplazables detrás de contratos propios.
- Lógica pura testeable y cancelación de extremo a extremo.

Se omiten topología, secretos y controles operativos internos.
