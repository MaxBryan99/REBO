CREATE DEFINER = `nano`@`%` PROCEDURE `SpDocBusGen`(IN validar INTEGER)
BEGIN
  if (validar=1) then
	select td.codigo,
	 td.nombre,
	 td.ncorto,
	 case Modulo WHEN 'COM' THEN 'COMPRAS' WHEN 'VEN' THEN 'VENTAS' WHEN 'ING' THEN 'INGRESOS' WHEN 'SAL' THEN 'SALIDAS' WHEN 'ALM' THEN 'ALMACEN'
	End as Modulo

	from tbldocumento td where td.est ='A';
ELSE
select td.codigo,
 td.nombre,
 td.ncorto,
 case Modulo WHEN 'COM' THEN 'COMPRAS' WHEN 'VEN' THEN 'VENTAS' WHEN 'ING' THEN 'INGRESOS' WHEN 'SAL' THEN 'SALIDAS' WHEN 'ALM' THEN 'ALMACEN'
End as Modulo

 from tbldocumento td ;
end if;
		 
END;