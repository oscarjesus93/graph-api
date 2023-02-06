USE [grupoapok-graph-api]
GO

/****** Object:  StoredProcedure [dbo].[SP_NODO_FATHER_DELETE]    Script Date: 5/2/2023 9:59:01 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[SP_NODO_FATHER_DELETE]
	@id int 
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

			IF Exists(SELECT ID FROM nodo_children WHERE parent = @id)
				BEGIN
					Set @message = 'No puede eliminar el nodo padre porque tiene nodos hijos asociados'
					Raiserror(@message, @severityError, @statusError)
					Return
				END
			

		DELETE FROM nodo_father WHERE id = @id
			
	
END
GO


