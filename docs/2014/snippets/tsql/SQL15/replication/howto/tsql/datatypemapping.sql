--<snippetsp_changecolumndatatype>
EXEC sp_changearticlecolumndatatype 
	@publication = 'OraPublication', 
	@publisher = 'OraPublisher', 
	@article = 'OraArticle', 
	@column = 'OraArticleCol', 
	@type = 'numeric', 
	@scale = 38, 
	@precision = 38;
GO
--</snippetsp_changecolumndatatype>

--<snippetsp_helpcolumndatatype_char>
EXEC sp_helpdatatypemap 
	@source_dbms = N'ORACLE', 
	@source_version = 9,
	@source_type = N'CHAR';
GO
--</snippetsp_helpcolumndatatype_char>

--<snippetsp_helpcolumndatatype_number>
EXEC sp_helpdatatypemap 
	@source_dbms = N'ORACLE', 
	@source_version = 9,
	@source_type = N'NUMBER',
	@defaults_only = 1;
GO
--</snippetsp_helpcolumndatatype_number>