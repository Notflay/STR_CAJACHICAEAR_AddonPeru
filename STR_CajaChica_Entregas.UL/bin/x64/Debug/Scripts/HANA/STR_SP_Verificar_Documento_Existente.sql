CREATE PROCEDURE STR_SP_VERIFICAR_DOCUMENTO_EXISTENTE
(
	NUMUNI VARCHAR(30),
	CARDCODE  VARCHAR(50)
)
AS
BEGIN
	SELECT COUNT('A') FROM OPCH WHERE LPAD("U_BPP_MDTD",2,'0')||LPAD("U_BPP_MDSD",4,'0')||LPAD("U_BPP_MDCD",15,'0')
	= NUMUNI AND "CardCode" = CARDCODE;
END

