DELIMITER $$
CREATE PROCEDURE `sp_GetStockBySymbol`(
    IN p_Symbol VARCHAR(10)
)
BEGIN
    SELECT * FROM Stocks WHERE Symbol = p_Symbol;
END$$
DELIMITER ;
