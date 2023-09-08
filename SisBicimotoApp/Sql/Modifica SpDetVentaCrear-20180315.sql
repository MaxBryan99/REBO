/*
Navicat MySQL Data Transfer

Source Server         : Local Server
Source Server Version : 100116
Source Host           : localhost:3306
Source Database       : bdbicimoto_alterno

Target Server Type    : MYSQL
Target Server Version : 100116
File Encoding         : 65001

Date: 2018-03-15 03:00:15
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Procedure structure for SpDetVentaCrear
-- ----------------------------
DROP PROCEDURE IF EXISTS `SpDetVentaCrear`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `SpDetVentaCrear`(IN nIdVenta VARCHAR(50),IN nCodigo VARCHAR(8),IN nMarca VARCHAR(3), IN nUnidad VARCHAR(3), IN nProced VARCHAR(3), IN nPVenta DOUBLE, IN nCantidad DOUBLE, IN nDcto DOUBLE, IN nIgv DOUBLE, IN nImporte DOUBLE, IN nAlmacen VARCHAR(3), IN nEmpresa VARCHAR(11), IN nTipPrecio VARCHAR(2), IN nTipImpuesto VARCHAR(2), IN nUserCreacion VARCHAR(15), IN nNOrden INTEGER, IN nDescrip VARCHAR(200))
BEGIN
	Insert into TblDetVenta(IdVenta,
													Codigo,
													Marca,
													Unidad,
													Proced,
													PVenta,
													Cantidad,
													Dcto,
													Igv,
													Importe,
													Almacen,
													Empresa,
													TipPrecio,
													TipImpuesto,
													FecCreacion,
													UserCreacion,
													Norden,
													DescripServ) 
					values (nIdVenta,
									nCodigo,
									nMarca,
									nUnidad,
									nProced ,
									nPVenta,
									nCantidad,
									nDcto,
									nIgv,
									nImporte,
									nAlmacen,
									nEmpresa,
									nTipPrecio,
									nTipImpuesto,
									NOW(),
									nUserCreacion,
									nNOrden,
									nDescrip);

	/* Actualiza Stock de Almacen */
	set @consulta = CONCAT("call SpStArticuloSalStock('",nEmpresa,"','",nAlmacen,"','",nCodigo,"',",nCantidad,",'",nUserCreacion,"')");
			prepare consulta from @consulta;
			execute consulta;
END
;;
DELIMITER ;
