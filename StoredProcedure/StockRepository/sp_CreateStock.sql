DELIMITER $$
CREATE PROCEDURE `sp_CreateStock`(
    IN p_Symbol VARCHAR(10),
    IN p_CompanyName VARCHAR(255),
    IN p_Purchase DECIMAL(18,2),
    IN p_LastDiv DECIMAL(18,2),
    IN p_Industry VARCHAR(100),
    IN p_MarketCap BIGINT
)
BEGIN
    INSERT INTO Stocks (Symbol, CompanyName, Purchase, LastDiv, Industry, MarketCap)
    VALUES (p_Symbol, p_CompanyName, p_Purchase, p_LastDiv, p_Industry, p_MarketCap);
    
    SELECT * FROM Stocks WHERE Id = LAST_INSERT_ID();
END$$
DELIMITER ;
