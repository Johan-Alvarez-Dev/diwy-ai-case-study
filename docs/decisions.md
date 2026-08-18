# Decisiones técnicas públicas

## Un núcleo, varios clientes

**Decisión:** .NET concentra la lógica; web/desktop usan HTTP+SSE y CLI usa NDJSON. **Motivo:** evita implementaciones divergentes. **Costo:** contratos de transporte estables.

## Contrato neutral de providers

**Decisión:** adaptar cada proveedor a tipos propios. **Motivo:** casos de uso independientes del SDK. **Costo:** extensiones controladas para capacidades exclusivas.

## Permisos antes de ejecución

**Decisión:** resolver `allow`, `deny` o `ask`. **Motivo:** una respuesta del modelo no es autorización. **Costo:** estados de confirmación adicionales.

## Operaciones reversibles

**Decisión:** representar cambios como propuestas y checkpoints. **Motivo:** reducir el costo de una ejecución incorrecta.
