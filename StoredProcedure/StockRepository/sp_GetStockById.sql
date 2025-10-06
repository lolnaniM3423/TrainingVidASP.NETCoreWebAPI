DELIMITER $$
CREATE PROCEDURE `sp_GetStockById`(
    IN p_Id INT
)
BEGIN
    -- First result set: The Stock itself
    SELECT * FROM Stocks WHERE Id = p_Id;
    
    -- Second result set: The Comments for that Stock
    SELECT * FROM Comments WHERE StockId = p_Id;
END$$
DELIMITER ;
