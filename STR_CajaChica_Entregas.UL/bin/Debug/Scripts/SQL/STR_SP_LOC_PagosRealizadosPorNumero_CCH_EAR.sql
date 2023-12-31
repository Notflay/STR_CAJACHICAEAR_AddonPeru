CREATE PROC STR_SP_LOC_PagosRealizadosPorNumero_CCH_EAR 
	@CodCCH varchar(20),
	@NmrCCH varchar(50),
	@TpoRnd varchar(3)
AS
DECLARE @MNDLOC CHAR(3)
SET @MNDLOC = (SELECT TOP 1 MainCurncy FROM OADM)
--Facturas 
	SELECT T2.CardCode [Cod. Proveedor]
	,T2.CardName [Nombre]
	,T1.DocEntry [DED]
	,T2.DocNum [Nro Documento]
	,CASE InvType WHEN '18' THEN 'FA' END [Documento]
	,T2.U_BPP_MDTD + '-' + T2.U_BPP_MDSD + '-' + T2.U_BPP_MDCD as [Numero SUNAT]
	,T2.DocDate [Fecha de Contabilizacion]
	,T2.TaxDate[Fecha de Documento]
	,T2.DocCur [Moneda de Documento]
	,CASE T2.DocCur when 'SOL' THEN T2.DocTotal ELSE T2.DocTotalFC END AS [Total]
	,T0.DocEntry [DEP]
	,T0.DocNum [Nro Pago]
	,T0.DocDate [Fecha de Pago]
	,T0.DocCurr [Moneda de Pago]
	,CASE T2.DocCur WHEN @MNDLOC THEN
		CASE T0.DocCurr WHEN @MNDLOC THEN T1.SumApplied ELSE (T1.SumApplied / (SELECT Rate FROM ORTT TX0 WHERE TX0.RateDate = T0.DocDate AND TX0.Currency =  T0.DocCurr)) END
	ELSE 
		CASE T0.DocCurr WHEN @MNDLOC THEN T1.SumApplied ELSE T1.AppliedFC END END AS [Importe Pagado]	
	FROM OVPM T0 INNER JOIN VPM2 T1 ON T0.DocEntry = T1.DocNum
	INNER JOIN OPCH T2 ON T1.DocEntry = T2.DocEntry
	WHERE U_BPP_TIPR = @TpoRnd AND T0.Canceled != 'Y' AND U_BPP_CCHI = @CodCCH AND U_BPP_NUMC = @NmrCCH
	
	UNION ALL
--Notas de Credito
	SELECT T2.CardCode [Cod. Proveedor]
	,T2.CardName [Nombre]
	,T1.DocEntry [DED]
	,T2.DocNum [Nro Documento]
	,CASE InvType WHEN '19' THEN 'NC' END [Documento]
	,T2.U_BPP_MDTD + '-' + T2.U_BPP_MDSD + '-' + T2.U_BPP_MDCD as [Numero SUNAT]
	,T2.DocDate [Fecha de Contabilizacion]
	,T2.TaxDate [Fecha de Documento]
	,T2.DocCur [Moneda de Documento]
	,CASE T2.DocCur when 'SOL' THEN T2.DocTotal ELSE T2.DocTotalFC END AS [Total]
	,T0.DocEntry [DEP]
	,T0.DocNum [Nro Pago]
	,T0.DocDate [Fecha de Pago]
	,T0.DocCurr [Moneda de Pago]
	,CASE T2.DocCur WHEN @MNDLOC THEN
		CASE T0.DocCurr WHEN @MNDLOC THEN T1.SumApplied ELSE (T1.SumApplied / (SELECT Rate FROM ORTT TX0 WHERE TX0.RateDate = T0.DocDate AND TX0.Currency =  T0.DocCurr)) END
	ELSE 
		CASE T0.DocCurr WHEN @MNDLOC THEN T1.SumApplied ELSE T1.AppliedFC END END AS [Importe Pagado]
	FROM ORCT T0 INNER JOIN RCT2 T1 ON T0.DocEntry = T1.DocNum
	INNER JOIN ORPC T2 ON T1.DocEntry = T2.DocEntry
	WHERE U_BPP_TIPR = @TpoRnd AND T0.Canceled != 'Y' AND U_BPP_CCHI = @CodCCH AND U_BPP_NUMC = @NmrCCH


	


	
	


