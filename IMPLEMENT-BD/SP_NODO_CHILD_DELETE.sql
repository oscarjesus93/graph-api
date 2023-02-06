
CREATE PROCEDURE [dbo].[SP_NODO_CHILD_DELETE]
	@id int
AS
BEGIN
	
	Declare @message As NVarchar(250) = '',
				    @severityError As Int = 12,
					@statusError As Int = 2			
				
	

			DELETE FROM nodo_children where id = @id		
			
	
END
GO


