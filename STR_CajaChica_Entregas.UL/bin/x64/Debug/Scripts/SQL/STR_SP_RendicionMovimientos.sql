CREATE PROCEDURE STR_SP_RendicionMovimientos --'CCHLIM001-15-001'
-- =============================================
-- Description:	DEVUELVE UNA CONSULTA DE LOS MOVIMIENTOS DE CAJACHICA/ENTREGA A RENDIR
-- =============================================
	@NUMEROCAJA varchar(50)
AS
	DECLARE @MONEDALOCAL VARCHAR(6)
	DECLARE @MONEDACAJA VARCHAR(6)
	SET @MONEDALOCAL = (SELECT MainCurncy FROM OADM)
	SET @MONEDACAJA =(SELECT U_CC_MNDA FROM [@STR_CCHAPR] T0 INNER JOIN [@STR_CCHAPRDET] T1 ON T0.DocEntry = T1.DocEntry WHERE U_CC_NMCC = @NUMEROCAJA)
	SELECT 
		TIPOCONT	
		,OBJTYPE
		,DOCENTRY
		,C
		,C0
		,C1
		,C2
		,C3
		,C4
		,CASE C5 WHEN '0' THEN '' ELSE C5 END as C5
		,CASE C6 WHEN 0 THEN '' ELSE (SELECT U_CC_MNDA FROM [@STR_CCHAPR] TX0 INNER JOIN [@STR_CCHAPRDET] TX1 
					ON TX0.DocEntry = TX1.DocEntry WHERE U_CC_NMCC = @NUMEROCAJA) + ' ' +CONVERT(varchar,ROUND(C6,3)) END AS C6
		,C7
		,C6 AS C8
		,C9
	 FROM 
		(
			SELECT
				'A0 - Monto Apertura' as TipoCont
				,'' as ObjType
				,'' as DocEntry
				,'' AS C
				, '' AS C0
				, T1.U_CC_NMCC + ' - ' + T0.Name + ' - ' + (SELECT AcctName FROM OACT TX0 WHERE TX0.AcctCode = T0.U_BPP_ACCT) AS C1
				, CONVERT(VARCHAR(10),T2.CreateDate,103) AS C2
				, '' AS C3
				, 0 AS C4
				, T0.U_BPP_TIPM + ' ' + CONVERT(varchar,ROUND(T1.U_CC_MNTO,3))  AS C5
				, 0 AS C6
				,'' as C7 
				, '01/01/1900' AS FECHAOP
				, 0 AS TIMEOP
				, ROUND(T1.U_CC_MNTO,3) as C9
			FROM [@BPP_CAJASCHICAS] T0
			INNER JOIN [@STR_CCHAPRDET] T1 ON T0.Code=T1.U_CC_CJCH 
			INNER JOIN [@STR_CCHAPR] T2 ON T1.DocEntry = T2.DocEntry					
			WHERE T1.U_CC_NMCC= @NUMEROCAJA
				UNION ALL
			SELECT 
				'A0 - Monto Apertura' as TipoCont
				, '' as ObjType
				, '' as DocEntry
				, '' AS C
				, '' AS C0
				, 'Saldo Anterior - ' + T1.U_CC_NMCC + ' - ' + T0.Name + ' - ' + (SELECT AcctName FROM OACT TX0 WHERE TX0.AcctCode = T0.U_BPP_ACCT) AS C1
				, CONVERT(VARCHAR(10),T2.CreateDate,103) AS C2
				, '' AS C3
				, 0 AS C4
				, T0.U_BPP_TIPM + ' ' + CONVERT(VARCHAR,ROUND(T1.U_CC_MNTR,3))  AS C5
				, 0 AS C6
				, '' as C7
				, '01/01/1900' AS FECHAOP
				, 0 AS TIMEOP
				, ROUND(T1.U_CC_MNTR,3) as C9
			FROM [@BPP_CAJASCHICAS] T0
			INNER JOIN [@STR_CCHAPRDET] T1 ON T0.Code=T1.U_CC_CJCH 
			INNER JOIN [@STR_CCHAPR] T2 ON T1.DocEntry = T2.DocEntry
			WHERE T1.U_CC_NMCC= @NUMEROCAJA
				UNION ALL
			SELECT
	 			'Carga de Documentos' as TipoCont
				,'' as ObjType
				,'' as DocEntry			
				,'DOC' as C
				, '' as C0
				, ISNULL((SELECT UPPER(LEFT(U_BPP_TDDD,3)) FROM [@BPP_TPODOC] WHERE Code=T2.U_BPP_MDTD)+' '+T2.U_BPP_MDSD+'-'+T2.U_BPP_MDCD, '') + ' - ' + T2.CardCode AS C1  
				, CONVERT(VARCHAR(10),T2.DocDate,103) AS C2
				, T2.DocCur + ' ' +CASE T2.DocCur WHEN 'SOL' THEN CONVERT(VARCHAR,ROUND(T2.DocTotal,3)) ELSE 
					CONVERT(VARCHAR,ROUND(T2.DocTotalFC,3)) END AS C3			
				, CASE T2.DocCur WHEN @MONEDALOCAL THEN 
					CASE @MONEDACAJA WHEN @MONEDALOCAL THEN
						1
					ELSE
						T0.DocCurr
					END
				  ELSE
					CASE @MONEDACAJA WHEN @MONEDALOCAL THEN
						(SELECT Rate FROM ORTT WHERE RateDate = T0.DocDate)
					ELSE
						1
					END
				  END as C4					
				, '0' AS C5			
				, CASE T0.DocCurr WHEN 'SOL' THEN CashSum ELSE T0.CashSumFC END AS C6
				, T2.Comments as C7
				, CONVERT(VARCHAR(10),T2.DocDate,103) AS FECHAOP
				, 0 AS TIMEOP
				, 0.000 as C9
			FROM OVPM T0 INNER JOIN VPM2 T1 ON T0.DocEntry = T1.DocNum
			INNER JOIN OPCH T2 ON T1.DocEntry = T2.DocEntry
			WHERE T0.U_BPP_TIPR = 'CCH' AND T0.DataSource = 'O' AND T2.DataSource = 'O' AND T0.U_BPP_NUMC = @NUMEROCAJA AND T0.Canceled != 'Y'
			UNION ALL
			--Pago de Facturas
			SELECT 
				'Otras Operaciones' as TipoCont
				,'' as ObjType
				,'' as DocEntry			
				,'DOC' as C
				, '' as C0
				, ISNULL((SELECT UPPER(LEFT(U_BPP_TDDD,3)) FROM [@BPP_TPODOC] WHERE Code=T2.U_BPP_MDTD)+' '+T2.U_BPP_MDSD+'-'+T2.U_BPP_MDCD, '') + ' - ' + T2.CardCode AS C1  
				, CONVERT(VARCHAR(10),T2.DocDate,103) AS C2
				, T2.DocCur + ' ' +CASE T2.DocCur WHEN 'SOL' THEN CONVERT(VARCHAR,ROUND(T2.DocTotal,3)) ELSE 
					CONVERT(VARCHAR,ROUND(T2.DocTotalFC,3)) END AS C3			
				, CASE T2.DocCur WHEN @MONEDALOCAL THEN 
					CASE @MONEDACAJA WHEN @MONEDALOCAL THEN
						1
					ELSE
						T0.DocCurr
					END
				  ELSE
					CASE @MONEDACAJA WHEN @MONEDALOCAL THEN
						(SELECT Rate FROM ORTT WHERE RateDate = T0.DocDate)
					ELSE
						1
					END
				  END as C4					
				, '0' AS C5			
				, CASE T0.DocCurr WHEN 'SOL' THEN CashSum ELSE T0.CashSumFC END AS C6
				, T2.Comments as C7
				, CONVERT(VARCHAR(10),T2.DocDate,103) AS FECHAOP
				, 0 AS TIMEOP
				, 0.000 as C9
			FROM OVPM T0 INNER JOIN VPM2 T1 ON T0.DocEntry = T1.DocNum
			INNER JOIN OPCH T2 ON T1.DocEntry = T2.DocEntry
			WHERE U_BPP_TIPR = 'CCH' AND T0.DataSource != 'O' AND T2.DataSource != 'O' AND T0.U_BPP_NUMC = @NUMEROCAJA AND T0.Canceled != 'Y'
				UNION ALL
			--Pagos a cuenta
			SELECT  
				'Otras Operaciones' as TipoCont
				,'' as ObjType
				,'' as DocEntry			
				,'CTA' as C
				, '' as C0
				, (SELECT FormatCode FROM OACT WHERE AcctCode = T1.AcctCode) + ' - ' + T1.AcctName AS C1  
				, CONVERT(VARCHAR(10),T0.DocDate,103) AS C2
				, T0.DocCurr + ' ' +CASE T0.DocCurr WHEN 'SOL' THEN CONVERT(VARCHAR,ROUND(T1.SumApplied,3)) ELSE 
					CONVERT(VARCHAR,ROUND(T1.AppliedFC,3)) END AS C3			
				, CASE T0.DocCurr 
							WHEN @MONEDALOCAL THEN 
								1 
							ELSE 							
								T0.DocRate
				  END as C4					
				, '0' AS C5			
				, CASE T0.DocCurr WHEN 'SOL' THEN CashSum ELSE T0.CashSumFC END AS C6
				, T0.Comments as C7
				, CONVERT(VARCHAR(10),T0.DocDate,103) AS FECHAOP
				, 0 AS TIMEOP
				, 0.000 as C9
			FROM OVPM T0 INNER JOIN VPM4 T1 ON T0.DocEntry = T1.DocNum
			WHERE U_BPP_TIPR = 'CCH' AND T0.DataSource != 'O' AND T0.Canceled != 'Y' AND T0.U_BPP_NUMC = @NUMEROCAJA
				UNION ALL
			SELECT 
				'Z92 - Totales' as TipoCont
				,''as ObjType
				,'' as DocEntry
				,'' AS C
				,'-2' AS C0
				,'TOTAL DE EGRESOS' AS C1
				,'' AS C2
				,'' AS C3
				,0 AS C4
				,'0' AS C5
				,SUM(C6) AS C6
				,'' as C7
				,'1/1/2999' as FECHAOP
				,(SELECT MAX(TransId) + 2 FROM OJDT) AS TIMEOP 
				, 0.000 as C9
			FROM(
					SELECT CASE T0.DocCurr WHEN 'SOL' THEN SUM(T0.CashSum) ELSE SUM(T0.CashSumFC)END AS C6
					FROM OVPM T0 WHERE T0.U_BPP_NUMC = @NUMEROCAJA AND T0.Canceled != 'Y'
					GROUP BY T0.DocCurr	
					UNION ALL 
					SELECT U_CC_MNTR AS C6 FROM [@STR_CCHAPRDET] WHERE U_CC_TRSL = @NUMEROCAJA
				) AS TOTAL
				UNION ALL
			SELECT
				'Z92 - Totales' as TipoCont
				,''as ObjType,'' as DocEntry
				,'' AS C
				,'-2' AS C0
				,'SALDO DE CAJA' AS C1
				,'' AS C2
				,'' AS C3
				,0 AS C4
				,CONVERT(VARCHAR,ROUND(CASE (SELECT COUNT('A') FROM [@STR_CCHAPRDET] WHERE U_CC_TRSL = T0.U_CC_NMCC) 
					WHEN 0 THEN 
						CASE T1.U_CC_MNDA
							WHEN 'SOL' THEN 
								T0.U_CC_MNAP - (SELECT ISNULL(SUM(CashSum),0.0) FROM OVPM TX0 WHERE TX0.U_BPP_NUMC = T0.U_CC_NMCC) 
							ELSE 
								T0.U_CC_MNAP - (SELECT ISNULL(SUM(CashSumFC),0.0) FROM OVPM TX0 WHERE TX0.U_BPP_NUMC = T0.U_CC_NMCC)
						END 
					ELSE 
						0
					END,3)) AS C5
				,CASE (SELECT COUNT('A') FROM [@STR_CCHAPRDET] WHERE U_CC_TRSL = T0.U_CC_NMCC)
					WHEN 0 THEN 0
					ELSE 
						CASE T1.U_CC_MNDA
							WHEN 'SOL' THEN 
								T0.U_CC_MNAP - (SELECT ISNULL(SUM(CashSum),0.0) FROM OVPM TX0 WHERE TX0.U_BPP_NUMC = T0.U_CC_NMCC)  
							ELSE 
								T0.U_CC_MNAP - (SELECT ISNULL(SUM(CashSumFC),0.0) FROM OVPM TX0 WHERE TX0.U_BPP_NUMC = T0.U_CC_NMCC)
						END 
					END
				AS C6
				,'' as C7
				,'01/01/2999' as FECHAOP
				,(SELECT MAX(TransId) + 2 FROM OJDT) AS TIMEOP 
				,CASE (SELECT COUNT('A') FROM [@STR_CCHAPRDET] WHERE U_CC_TRSL = T0.U_CC_NMCC) 
					WHEN 0 THEN 
						CASE T1.U_CC_MNDA
							WHEN 'SOL' THEN 
								T0.U_CC_MNAP - (SELECT ISNULL(SUM(CashSum),0.0) FROM OVPM TX0 WHERE TX0.U_BPP_NUMC = T0.U_CC_NMCC)  
							ELSE 
								T0.U_CC_MNAP - (SELECT ISNULL(SUM(CashSumFC),0.0) FROM OVPM TX0 WHERE TX0.U_BPP_NUMC = T0.U_CC_NMCC)
						END 
					ELSE 
						0
					END AS C9 
			FROM [@STR_CCHAPRDET] T0 INNER JOIN [@STR_CCHAPR] T1 
			ON T0.DocEntry = T1.DocEntry  
			WHERE T0.U_CC_NMCC = @NUMEROCAJA
				UNION ALL
			SELECT 
				'Z91 - Traspaso' as TipoCont
				,''as ObjType
				,'' as DocEntry
				,'TR' AS C
				,'' AS C0
				,'TRASP.  ' + T0.U_CC_NMCC AS C1
				,CONVERT(VARCHAR(10),T1.CreateDate,103) AS C2
				,T1.U_CC_MNDA + ' ' + CONVERT(VARCHAR,ROUND(T0.U_CC_MNTR,3)) AS C3
				,1 AS C4
				,'0' AS C5
				,T0.U_CC_MNTR AS C6
				,'' as C7
				,CONVERT(VARCHAR(10),T1.CreateDate,103) AS FECHAOP
				,(SELECT MAX(TransId) FROM OJDT) AS TIMEOP 
				, 0.000 as C9
			FROM [@STR_CCHAPRDET] T0 INNER JOIN [@STR_CCHAPR] T1
			ON T0.DocEntry = T1.DocEntry
			WHERE T0.U_CC_TRSL = @NUMEROCAJA AND U_CC_STDO IN ('A','C')
		) AS TAB