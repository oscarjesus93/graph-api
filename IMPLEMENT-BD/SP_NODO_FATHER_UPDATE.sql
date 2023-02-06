
CREATE PROCEDURE [dbo].[SP_NODO_FATHER_UPDATE]
	@id int, 
	@title nvarchar(150) = ''
AS
BEGIN
	
	Declare @message As NVarchar(250) = '',
				    @severityError As Int = 12,
					@statusError As Int = 2			
				
			IF(@id = '')

				BEGIN
                
					Set @message = 'El id es obligatorio'
					Raiserror(@message, @severityError, @statusError)
					Return

				END	

			IF NOT Exists(SELECT ID FROM nodo_father WHERE id = @id)

				BEGIN
                
					Set @message = 'El registro a actualizar no existe'
					Raiserror(@message, @severityError, @statusError)
					Return

				END	
			

			UPDATE [nodo_father] SET title = @title, created_at = GETDATE() WHERE [ID] = @id
 
			SELECT  
				 [id], [title], [created_at]
			FROM 
				[nodo_father]
			WHERE [ID] = @id
	
END
GO


