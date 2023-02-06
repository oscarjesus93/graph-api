
CREATE PROCEDURE [dbo].[SP_NODO_FATHER_INSERT]

AS
BEGIN
	
	Declare @message As NVarchar(250) = '',
			@severityError As Int = 12,
			@statusError As Int = 2,
			@valorid as int = 0			
			
			INSERT INTO nodo_father (created_at)
			
			VALUES
			   (getdate())

			SELECT TOP 1
				 @valorid = [id]
			FROM 
				[nodo_father]
			ORDER BY [id] DESC

			UPDATE nodo_father set title = CONVERT(varchar(10), @valorid) where id = @valorid

			SELECT [id], [title], [created_at] FROM nodo_father WHERE ID = @valorid
	
END
GO


