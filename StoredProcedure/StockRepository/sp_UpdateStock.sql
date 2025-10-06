DELIMITER $$
CREATE PROCEDURE `sp_UpdateStock`(
    IN p_Id INT,
    IN p_Symbol VARCHAR(10),
    IN p_CompanyName VARCHAR(255),
    IN p_Purchase DECIMAL(18,2),
    IN p_LastDiv DECIMAL(18,2),
    IN p_Industry VARCHAR(100),
    IN p_MarketCap BIGINT
)
BEGIN
    UPDATE Stocks
    SET
        Symbol = p_Symbol,
        CompanyName = p_CompanyName,
        Purchase = p_Purchase,
        LastDiv = p_LastDiv,
        Industry = p_Industry,
        MarketCap = p_MarketCap
    WHERE Id = p_Id;
    
    SELECT * FROM Stocks WHERE Id = p_Id;
END$$
DELIMITER ;
