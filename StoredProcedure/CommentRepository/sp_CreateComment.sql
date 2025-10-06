DELIMITER $$
CREATE PROCEDURE `sp_CreateComment`(
    IN p_Title VARCHAR(255),
    IN p_Content TEXT,
    IN p_StockId INT,
    IN p_AppUserId VARCHAR(255)
)
BEGIN
    INSERT INTO Comments (Title, Content, CreatedOn, StockId, AppUserId)
    VALUES (p_Title, p_Content, NOW(), p_StockId, p_AppUserId);

    SET @NewId = LAST_INSERT_ID();

    SELECT c.*, u.* FROM Comments c LEFT JOIN AspNetUsers u ON c.AppUserId = u.Id WHERE c.Id = @NewId;
END$$
DELIMITER ;