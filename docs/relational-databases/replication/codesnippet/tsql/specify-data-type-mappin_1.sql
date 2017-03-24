EXEC sp_changearticlecolumndatatype 
	@publication = 'OraPublication', 
	@publisher = 'OraPublisher', 
	@article = 'OraArticle', 
	@column = 'OraArticleCol', 
	@type = 'numeric', 
	@scale = 38, 
	@precision = 38;
GO