 **sys.index_resumable_operations** is a system view that monitors and checks the current execution status for resumable Index rebuild.  
  **Applies to**: SQL Server vNext (feature is in public preview)
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**object_id**|**int**|ID of the object to which this index belongs (not nullable).|  
|**index_id**|**int**|ID of the index (not nullable). **index_id** is unique only within the object.|
|**name**|**sysname**|Name of the index. **name** is unique only within the object.|  
|**sql_text**|**nvarchar(max)**|DDL T-SQL statement text|
|**last_max_dop**|**smallint**|Last MAX_DOP used (default = 0)|
|**partition_number**|**int**|Partition number within the owning index or heap. For non-partitioned tables and indexes or in case all partitions are being rebuild the value of this column is NULL.|
|**state**|**tinyint**|Operational state for resumable index:<br /><br />0=Running<br /><br />1=Pause|
|**state_desc**|**nvarchar(60)**|Description of the operational state for resumable index (running or Paused)|  
|**start_time**|**datetime**|Index operation start time (not nullable)|
|**last_pause_time**|**datatime**| Index operation last pause time (nullable). NULL if operation is running and never paused.|
|**total_execution_time**|**int**|Total execution time from start time in minutes (not nullable)|
|**percent_complete**|**real**|Index operation progress completion in % ( not nullable).|
|**page_count**|**bigint**|Total number of index pages allocated by the index build operation for the new and mapping indexes ( not nullable ). 


## Permissions  
 [!INCLUDE[ssCatViewPerm](../Token/ssCatViewPerm_md.md)] For more information, see [Metadata Visibility Configuration](../Topic/Metadata%20Visibility%20Configuration.md).  
  
## Example  
 List all resumable indexe rebuild operationas that are in the PAUSE state. 
  
```  
SELECT * FROM  sys.index_resumable_operations WHERE STATE = 1;  
```  
  
## See Also 
 [ALTER INDEX &#40;Transact-SQL&#41;](ALTER%20INDEX%20\(Transact-SQL\).md)    
 [Catalog views &#40;Transact-SQL&#41;](Catalog%20Views%20\(Transact-SQL\).md)
 [Object catalog views &#40;Transact-SQL&#41;](Object%20Catalog%20Views%20\(Transact-SQL\).md)
 [sys.indexes &#40;Transact-SQL&#41;](sys.indexes%20\(Transact-SQL\).md) 
 [sys.index_columns &#40;Transact-SQL&#41;](../Topic/sys.index_columns%20\(Transact-SQL\).md)   
 [sys.xml_indexes &#40;Transact-SQL&#41;](../Topic/sys.xml_indexes%20\(Transact-SQL\).md)   
 [sys.objects &#40;Transact-SQL&#41;](../Topic/sys.objects%20\(Transact-SQL\).md)   
 [sys.key_constraints &#40;Transact-SQL&#41;](../Topic/sys.key_constraints%20\(Transact-SQL\).md)   
 [sys.filegroups &#40;Transact-SQL&#41;](../Topic/sys.filegroups%20\(Transact-SQL\).md)   
 [sys.partition_schemes &#40;Transact-SQL&#41;](../Topic/sys.partition_schemes%20\(Transact-SQL\).md)   
 [Querying the SQL Server System Catalog FAQ](Querying%20the%20SQL%20Server%20System%20Catalog%20FAQ.md)   
  
  