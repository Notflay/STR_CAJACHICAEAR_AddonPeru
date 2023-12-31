CREATE PROCEDURE STR_SP_INT_AgruparLineasCCH
(
	DOCENT INT,
	CODPRV VARCHAR(20),
	TPODOC VARCHAR(20),
	SREDOC VARCHAR(20),
	CORDOC VARCHAR(50)
)
AS
	LINEAS VARCHAR(1000);
BEGIN
	SELECT STRING_AGG("LineId",',') INTO LINEAS FROM "@STR_CCHCRGDET" T0 INNER JOIN "@STR_CCHCRG" T1 ON T0."DocEntry" = T1."DocEntry"
	WHERE T1."DocEntry" = :DOCENT AND "U_CC_CDPV" = :CODPRV AND "U_CC_TDOC" = :TPODOC  AND "U_CC_SDOC" = :SREDOC  AND "U_CC_CDOC" = :CORDOC AND "U_CC_SLCC" = 'Y';
	SELECT :LINEAS FROM DUMMY;
END
