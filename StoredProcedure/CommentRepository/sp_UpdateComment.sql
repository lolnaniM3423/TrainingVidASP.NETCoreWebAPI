DELIMITER $$
CREATE PROCEDURE `sp_UpdateComment`(
    IN p_Id INT,
    IN p_Title VARCHAR(255),
    IN p_Content TEXT
)
BEGIN
    UPDATE Comments
    SET
        Title = p_Title,
        Content = p_Content
    WHERE Id = p_Id;

    SELECT c.*, u.* FROM Comments c LEFT JOIN AspNetUsers u ON c.AppUserId = u.Id WHERE c.Id = p_Id;
END$$
DELIMITER ;