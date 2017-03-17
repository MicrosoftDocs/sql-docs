SELECT f.* FROM sysmergesubsetfilters AS f 
INNER JOIN sysmergepublications AS p
ON f.pubid = p.pubid WHERE p.[name] = @publication;