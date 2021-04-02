> [!NOTE]
> Since reading JSON files is supported as reading CSV format with added `fieldterminator` and `fieldquote` options ([visit this link for more information](https://docs.microsoft.com/azure/synapse-analytics/sql/query-json-files)), in order to create external file format for JSON files, please use syntax from delimited tab with appropriate `FIELD_TERMINATOR` and `STRING_DELIMITER` options for your JSON file. <br>
_Info: Synapse Serverless SQL pool supports GZIPPED JSON files._
