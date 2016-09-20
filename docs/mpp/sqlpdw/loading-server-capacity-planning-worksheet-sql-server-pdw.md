---
title: "Loading Server Capacity Planning Worksheet (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: df2155be-a624-40ba-9a85-58af708f7ce7
caps.latest.revision: 9
author: BarbKess
---
# Loading Server Capacity Planning Worksheet (SQL Server PDW)
This capacity planning worksheet helps you to determine the requirements for a loading server for loading data into SQL Server PDW. Use this to create your plan for purchasing or provisioning existing loading servers.  
  
## Capacity Planning Worksheet for Loading Servers  
**Worksheet Notes:**  
  
1.  This worksheet applies to servers that will be loading data with the **dwloader** Command-Line Loading Tool.  
  
2.  For loading data with Integration Services or a third party loading tool, the requirements can vary depending upon differences in the loading process.  
  
3.  Most of the requirements apply to loading either compressed or uncompressed data files; any differences in requirements are noted in bold.  
  
![icon used in topic](../../mpp/sqlpdw/media/SQL_Server_PDW_clipboard_icon.png "SQL_Server_PDW_clipboard_icon")**Capacity Planning Worksheet**  
  
Print this worksheet and fill it in with your own requirements.  
  
|Component|Requirement|Fill in this column with your own requirements|Recommendations|  
|-------------|---------------|--------------------------------------------------|-------------------|  
|Storage|Maximum bytes you plan to store on the Loading server at any given period of time.|![pencil icon for topic](../../mpp/sqlpdw/media/SQL_Server_PDW_pencil_icon.png "SQL_Server_PDW_pencil_icon")|To determine storage requirements, figure out how much data you plan to store on the Loading server at any given period of time.  Capacity requirements are for load files only; the operating system and the load files should be on different disk arrays.<br /><br />For example: If you plan to load 100GB of data from disk 3 times each day, but not delete the data files until the end of the week, then you would need a minimum 2.1TB of storage to store the data files. We recommend being conservative and getting about 30% more to account for variances and growth.  So for this example 2.73TB of storage space would be better.|  
|Load Rate|Maximum bytes per hour of data to be loaded into SQL Server PDW.|![pencil icon for topic](../../mpp/sqlpdw/media/SQL_Server_PDW_pencil_icon.png "SQL_Server_PDW_pencil_icon")|This is an estimate. When computing this requirement, assume that the files are already on the Loading server, and that other loading conditions are as good as possible.<br /><br />For example: No need to factor in data compressibility since dwloader always sends uncompressed data to the SQL Server PDW. No need to factor in data type conversions and the size of the destination table.|  
|Network|Network connection type.|![pencil icon for topic](../../mpp/sqlpdw/media/SQL_Server_PDW_pencil_icon.png "SQL_Server_PDW_pencil_icon")|Determine the best network connection type that can meet your load rate requirements.<br /><br />For example: InfiniBand or 10Gbit Ethernet will provide the optimal loading rates. 1Gbit Ethernet will limit load rates to 360 GB per hour or less.|  
|I/O|Bytes per hour for reads and writes.|![pencil icon for topic](../../mpp/sqlpdw/media/SQL_Server_PDW_pencil_icon.png "SQL_Server_PDW_pencil_icon")|To load data, dwloader must read all of the data from disk before sending it to SQL Server PDW.<br /><br />Each loading server cannot load data faster than the appliance can receive data from all loading sources. To save money, plan the I/O read capacity for loading so that it does not exceed the load capacity of the appliance.<br /><br />For example:<br />                    SQL Server PDW can receive and load data into a 1-rack appliance at a maximum rate of 1.8 TB per hour. For an appliance with 2 or more racks, the maximum load rate is 3.6 TB per hour.<br /><br />If you plan to load from multiple loading servers at the same time, the I/O requirements for each loading server will be less than when one server is doing all the loading.<br /><br />For example: One loading server can load a maximum of 1.8 TB per hour for a 1-rack appliance. Two loading servers could each concurrently load 900 GB per hour into a 1-rack appliance. Higher levels of concurrency can reduce efficiency and maximum throughput.<br /><br />For I/O capacity, take into account all of the I/O happening on the loading server. If the Loading server has other I/O traffic in addition to data loads, such as receiving data files from an ETL server, the I/O requirements will increase.<br /><br />**For compressed data**, the I/O requirements depend on the data compression rate. **dwloader** reads the compressed data and then uncompresses it before sending it to SQL Server PDW. The higher the compression ratio, the less data the Loading server will need to read from disk.<br /><br />For example: If the required load rate is 1.8 TB per hour, and the data is stored on the Loading server with 2:1 compression, then the Loading server needs only to read 900 GB per hour from disk instead of 1.8 TB. A 3:1 compression ratio means the Loading server needs to read 600 GB per hour from disk.|  
|CPU|Number of sockets.|![pencil icon for topic](../../mpp/sqlpdw/media/SQL_Server_PDW_pencil_icon.png "SQL_Server_PDW_pencil_icon")|For loading uncompressed data, **dwloader** is not a CPU-intensive application.  As a minimum requirement, we recommend using a recently-manufactured 2-socket server.<br /><br />**For loading compressed data**, you need enough CPU power to uncompress the data before sending it to SQL Server PDW. **dwloader** can run 10 active threads at once. If you plan to load 10 compressed files concurrently, we recommend the server has at least a 10-core CPU or two 6-core CPUs.|  
|RAM|GB of memory that allows Windows to cache files during loads.|![pencil icon for topic](../../mpp/sqlpdw/media/SQL_Server_PDW_pencil_icon.png "SQL_Server_PDW_pencil_icon")|**dwloader** uses very little RAM on the Loading server. For performance, Windows uses memory to cache the loading files after they are read from disk.<br /><br />To determine the RAM requirements, refer to your Windows Server installation and any 3rd party application requirements. We recommend a minimum of 32GB if you do not have requirements from other sources.<br /><br />**For compressed data**, faster RAM is useful because it will speed up the decompression.|  
  
## See Also  
[Acquire and Configure a Loading Server &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/acquire-and-configure-a-loading-server-sql-server-pdw.md)  
[dwloader Command-Line Loader &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/dwloader-command-line-loader-sql-server-pdw.md)  
  
