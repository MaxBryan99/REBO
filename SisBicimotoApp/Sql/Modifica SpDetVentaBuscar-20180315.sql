/*
Navicat MySQL Data Transfer

Source Server         : Local Server
Source Server Version : 100116
Source Host           : localhost:3306
Source Database       : bdbicimoto_alterno

Target Server Type    : MYSQL
Target Server Version : 100116
File Encoding         : 65001

Date: 2018-03-15 03:05:09
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Procedure structure for SpDetVentaBuscar
-- ----------------------------
DROP PROCEDURE IF EXISTS `SpDetVentaBuscar`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `SpDetVentaBuscar`(IN nIdVenta VARCHAR(50), IN nRucEmpresa VARCHAR(11), IN nAlmacen VARCHAR(3))
BEGIN
	Select Ven.Codigo,
				 (Select art.Nombre from tblarticulos as art where art.CodArt = Ven.Codigo AND art.Almacen=nAlmacen and art.RucEmpresa=nRucEmpresa) as Nombre,
				 (select DescCorta from tbldetcatalogo where CodCatalogo = '010' and CodDetCat = Ven.Marca) as nomMarca,
				 (select DescCorta from tbldetcatalogo where CodCatalogo = '013' and CodDetCat = Ven.Unidad) as nomUnidad,
				 Ven.Cantidad,
				 Ven.PVenta,
				 Ven.Igv,
				 Ven.Importe,
				 (select DescCorta from tbldetcatalogo where CodCatalogo = '009' and CodDetCat = Ven.Proced) as nomProced,
					TipPrecio,
					TipImpuesto,
					Dcto,
					DescripServ
	from tbldetventa as Ven where Ven.IdVenta = nIdVenta and Ven.Empresa = nRucEmpresa and Ven.Almacen = nAlmacen order by Ven.Norden asc;
	
END
;;
DELIMITER ;
