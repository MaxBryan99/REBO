/*
Navicat MySQL Data Transfer

Source Server         : Local Server
Source Server Version : 100116
Source Host           : localhost:3306
Source Database       : bdbicimoto

Target Server Type    : MYSQL
Target Server Version : 100116
File Encoding         : 65001

Date: 2017-12-10 11:19:45
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Procedure structure for SpVentaBuscarData
-- ----------------------------
DROP PROCEDURE IF EXISTS `SpVentaBuscarData`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `SpVentaBuscarData`(IN nIdVenta VARCHAR(50), IN nRucEmpresa VARCHAR(11), IN nAlmacen VARCHAR(3))
BEGIN
	
Select DATE_FORMAT(Fecha,'%d/%m/%Y') Fec,
			Cliente,
			TipDocCli,
			Doc,
			Serie,
			Numero,
			TMoneda,
			NPedido,
			TCambio,
			TVenta,
			(Case TVenta When '01' then '' else NDias End) as ValDia,
			(Case TVenta When '01' then '' else FVence End) as FecVencimiento,
			TBruto,
			TExonerada,
			TInafecta,
			TGratuita,
			TIgv,
			Total,
			TEst,
			Est,
			Empresa,
			Almacen,
			Vendedor,
			Usuario,
			NomArchXml,
			FecCreacion,
			UserCreacion,
			FecModi,
			UserModi,
			EGratuita,
			TComp,
			Dcto,
			ArchivoXml
	from tblventa where IdVenta = nIdVenta and Empresa = nRucEmpresa and Almacen = nAlmacen;
	
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for SpDetVentaBuscarData
-- ----------------------------
DROP PROCEDURE IF EXISTS `SpDetVentaBuscarData`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `SpDetVentaBuscarData`(IN nIdVenta VARCHAR(50), IN nRucEmpresa VARCHAR(11), IN nAlmacen VARCHAR(3))
BEGIN
	Select IdVenta,
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
				FecModi,
				UserModi,
				Norden
	from tbldetventa as Ven where Ven.IdVenta = nIdVenta and Ven.Empresa = nRucEmpresa and Ven.Almacen = nAlmacen order by Ven.Norden asc;
	
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for SpClienteBuscarData
-- ----------------------------
DROP PROCEDURE IF EXISTS `SpClienteBuscarData`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `SpClienteBuscarData`(IN ncodigo VARCHAR(15), IN nRucEmpresa VARCHAR(11))
BEGIN
	SELECT RucDni,
				TipDoc,
				Nombre,
				Direccion,
				Telefono,
				Celular,
				Contacto,
				Email,
				DireccionEnvio,
				Region,
				Provincia,
				Distrito,
				LimCredito,
				CodVendedor,
				Est,
				FecCreacion,
				UserCreacion,
				(case FecModi when '0000-00-00 00:00:00' then '' else FecModi end) as FModi,
				UserModi,
				RucEmpresa 
				 from tblcliente cli where cli.RucDni = ncodigo and cli.RucEmpresa=nRucEmpresa;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for SpClienteCrearRemoto
-- ----------------------------
DROP PROCEDURE IF EXISTS `SpClienteCrearRemoto`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `SpClienteCrearRemoto`(IN nrucdni VARCHAR(11), IN nTipDoc VARCHAR(2), IN nnombre VARCHAR(150), IN ndirec VARCHAR(50), IN ntelef VARCHAR(50), IN ncelular VARCHAR(50), IN ncontacto VARCHAR(150), IN nemail VARCHAR(150), IN ndirecenvio VARCHAR(150), IN nregion VARCHAR(2), IN nprov VARCHAR(2), IN ndist VARCHAR(2), IN nlimcre DOUBLE, IN ncodvend VARCHAR(6), IN nuser VARCHAR(15), IN nFecCreacion VARCHAR(20), IN nest VARCHAR(1), IN nRucEmpresa VARCHAR(11), IN nIdventa VARCHAR(50))
BEGIN
	declare vmax LONG;
	DECLARE vCanCli LONG;
	DECLARE vcodcli VARCHAR(11);
	SET vmax = (Select Count(*) from tblcliente where rucdni = nrucdni and TipDoc = nTipDoc and RucEmpresa = nRucEmpresa);
	IF vmax is null THEN
			set vmax :=0;
	end if;
	IF (vmax = 0) THEN
		insert into tblCliente(RucDni,
												 TipDoc,
                         nombre, 
                         direccion, 
                         telefono,
                         Celular,
                         Contacto,
                         Email,
                         DireccionEnvio,
                         Region,
                         Provincia,
                         Distrito,
                         LimCredito,
                         CodVendedor,
                         UserCreacion,
                         FecCreacion,
                         est,
												 RucEmpresa) values 
              (nrucdni,
							 nTipDoc,
							 nnombre,
							 ndirec,
               ntelef,
               ncelular,
               ncontacto,
               nemail,
               ndirecenvio,
               nregion,
               nprov,
               ndist,
               nlimcre,
               ncodvend,
               nuser,
							 nFecCreacion,
							 nest,
							 nRucEmpresa);
	ELSE 
		IF (SUBSTR(nrucdni,1,1) = 'C') THEN
			IF (nrucdni='C0000000001') THEN
				SET vCanCli = (Select Count(*) from tblcliente where rucdni = nrucdni and TipDoc = nTipDoc and upper(Nombre) = UPPER(nnombre)and RucEmpresa = nRucEmpresa);
				IF (vCanCli = 0) THEN
					SET vmax = (Select Count(rucdni) from tblcliente where rucdni like 'C%' and RucEmpresa = nRucEmpresa);
					IF vmax is null THEN
						set vmax :=0;
					end if;
					/*SET vcoddetcat = vmax +1;*/
					SET vcodcli = right(concat('C000000000',substr(vmax + 1,1,11)),11);
					insert into tblCliente(RucDni,
												 TipDoc,
                         nombre, 
                         direccion, 
                         telefono,
                         Celular,
                         Contacto,
                         Email,
                         DireccionEnvio,
                         Region,
                         Provincia,
                         Distrito,
                         LimCredito,
                         CodVendedor,
                         UserCreacion,
                         FecCreacion,
                         est,
												 RucEmpresa) values 
              (vcodcli,
							 '6',
							 nnombre,
							 ndirec,
               ntelef,
               ncelular,
               ncontacto,
               nemail,
               ndirecenvio,
               nregion,
               nprov,
               ndist,
               nlimcre,
               ncodvend,
               nuser,
               NOW(),
							 nest,
							 nRucEmpresa);
							
							UPDATE TblVenta set Cliente = vcodcli, TipDocCli = nTipDoc where IdVenta = nIdventa;
							
				END IF;
			END IF;
		END IF;
	END IF;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for SpVentaCrearRemoto
-- ----------------------------
DROP PROCEDURE IF EXISTS `SpVentaCrearRemoto`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `SpVentaCrearRemoto`(IN nIdVenta VARCHAR(50), IN nFecha VARCHAR(10), IN nCliente VARCHAR(11), IN nTipDocCli VARCHAR(2), IN nDoc VARCHAR(3), IN nSerie VARCHAR(3), IN nNumero VARCHAR(11), IN nTMoneda VARCHAR(10), IN nNPedido VARCHAR(50), IN nTCambio DOUBLE, IN nTVenta VARCHAR(10), IN nNDias INTEGER, IN nFVence VARCHAR(10), IN nTBruto DOUBLE, IN nTExonerada DOUBLE, IN nTInafecta DOUBLE, IN nTGratuita DOUBLE, IN nTIgv DOUBLE, IN nTotal DOUBLE, IN nTEst VARCHAR(1), IN nEst VARCHAR(1), IN nEmpresa VARCHAR(11), IN nAlmacen VARCHAR(3), IN nVendedor VARCHAR(6), IN nUsuario VARCHAR(15), IN nUserCreacion VARCHAR(15), IN nEgratuita VARCHAR(1), IN nTComp VARCHAR(2), IN nDcto DOUBLE, IN nFecCreacion VARCHAR(15), IN nArchivo TEXT, IN nNomArchivo VARCHAR(50))
BEGIN
	insert into TblVenta(IdVenta,
											Fecha,
											Cliente,
											TipDocCli,
											Doc,
											Serie,
											Numero,
											TMoneda,
											NPedido,
											TCambio,
											TVenta,
											NDias,
											FVence,
											TBruto,
											TExonerada,
											TInafecta,
											TGratuita,
											TIgv,
											Total,
											TEst,
											Est,
											Empresa,
											Almacen,
											Vendedor,
											Usuario,
											FecCreacion,
											UserCreacion,
											Egratuita,
											TComp,
											Dcto,
											ArchivoXml,
											NomArchXml)
								values (nIdVenta,
												nFecha,
												nCliente,
												nTipDocCli,
												nDoc,
												nSerie,
												nNumero,
												nTMoneda,
												nNPedido,
												nTCambio,
												nTVenta,
												nNDias,
												nFVence,
												nTBruto,
												nTExonerada,
												nTInafecta,
												nTGratuita,
												nTIgv,
												nTotal,
												nTEst,
												nEst,
												nEmpresa,
												nAlmacen,
												nVendedor,
												nUsuario,
												nFecCreacion,
												nUserCreacion,
												nEgratuita,
												nTComp,
												nDcto,
												nArchivo,
												nNomArchivo);
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for SpDetVentaCrearRemoto
-- ----------------------------
DROP PROCEDURE IF EXISTS `SpDetVentaCrearRemoto`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `SpDetVentaCrearRemoto`(IN nIdVenta VARCHAR(50),IN nCodigo VARCHAR(8),IN nMarca VARCHAR(3), IN nUnidad VARCHAR(3), IN nProced VARCHAR(3), IN nPVenta DOUBLE, IN nCantidad DOUBLE, IN nDcto DOUBLE, IN nIgv DOUBLE, IN nImporte DOUBLE, IN nAlmacen VARCHAR(3), IN nEmpresa VARCHAR(11), IN nTipPrecio VARCHAR(2), IN nTipImpuesto VARCHAR(2), IN nUserCreacion VARCHAR(15), IN nNOrden INTEGER, IN nFecCreacion VARCHAR(15))
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
													Norden) 
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
									nFecCreacion,
									nUserCreacion,
									nNOrden);

	/* Actualiza Stock de Almacen */
	/*set @consulta = CONCAT("call SpStArticuloSalStock('",nEmpresa,"','",nAlmacen,"','",nCodigo,"',",nCantidad,",'",nUserCreacion,"')");
			prepare consulta from @consulta;
			execute consulta;*/
END
;;
DELIMITER ;