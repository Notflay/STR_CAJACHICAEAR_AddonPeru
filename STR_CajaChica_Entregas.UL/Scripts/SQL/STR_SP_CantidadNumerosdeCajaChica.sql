CREATE PROCEDURE STR_SP_CantidadNumerosdeCajaChica
	@NROCCH VARCHAR(50)
AS
BEGIN
	SELECT COUNT('A') FROM [@STR_CCHCRG] WHERE U_CC_NMRO = @NROCCH
END