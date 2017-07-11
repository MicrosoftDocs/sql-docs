---

title: "What's new in Analytics Platform System – a scale-out data warehouse"
description: "See what’s new in Microsoft® Analytics Platform System, a scale-out on-premises appliance that hosts MPP SQL Server Parallel Data Warehouse."

author: "happynicolle" 
ms.author: "nicw;barbkess"
ms.date: "11/28/2016"
ms.topic: "article"

---

# What's new in Analytics Platform System 2016, a scale-out MPP data warehouse
See what’s new in Microsoft® Analytics Platform System (APS) 2016, the latest Appliance Update for a scale-out on-premises appliance that hosts MPP SQL Server Parallel Data Warehouse. 

## SQL Server 2016

APS 2016 runs on the latest SQL Server 2016 release and uses the default database compatibility level 130.  SQL Server 2016 makes it possible to support some of the  new features such as secondary indexes for clustered columnstore indexes and Kerberos for PolyBase. 


## T-SQL
APS 2016 supports these T-SQL compatibility improvements.  These additional language elements make it easier to migrate from SQL Server and other data sources. 

- [Column-level SQL collations][] are now supported in addition to Windows collations.
- [Nonclustered indexes on clustered columnstore indexes][] improve performance of queries that search for specific values in the clustered columnstore index. 
- [SELECT...INTO][] 
- [sp_spaceused()][] displays the disk space used or reserved in a table or database.
- [Wide tables][] support is the same as SQL Server 2016. The previous limit of 32K for the row size no longer exists. 

### Data types

- [VARCHAR(MAX)][], [NVARCHAR(MAX)][] and [VARBINARY(MAX)][]. These LOB data types have a maximum size of 2 GB. To load these objects use [bcp Utility][]. Polybase and dwloader do not currently support these data types. 
- [SYSNAME][]
- [UNIQUEIDENTIFIER][]
- [NUMERIC][] and DECIMAL data types.

### Window functions

- [ROWS or RANGE][] in the OVER clause of the SELECT statement.
- [FIRST_VALUE][]
- [LAST_VALUE][]
- [CUME_DIST][]
- [PERCENT_RANK][]

### Security functions

- [CHECKSUM()][] and [BINARY_CHECKSUM()][]
- [HAS_PERMS_BY_NAME()][]

### Additional functions

- [NEWID()][]
- [RAND()][]

## PolyBase/Hadoop enhancements

- Compatibility with Hortonworks HDP 2.4 and HDP 2.5
- Kerberos support via database scoped credentials
- Credential support with Azure Storage Blobs

## Install and upgrade enhancements

### Enterprise architecture updates
Upgrading your existing appliance to APS 2016 installs the latest firmware and driver updates, which include security fixes. 

A new appliance from HPE or DELL includes all the latest updates plus:

- Latest generation processor support (Broadwell)
- Update to DDR4 DIMMs
- Improved DIMM throughput

### Integration

- Fully Qualified Domain Name (FQDN) support makes it possible to setup a Domain trust to the appliance. 
- To use FQDN, you need to do a full upgrade and opt-in during the upgrade. 

### Reduced downtime
Installing or upgrading to APS 2016 is faster and requires less downtime than previous releases. To reduce downtime, the install or upgrade: 

 - Streamlines applying WSUS updates by using an image that contains all the updates through June 2016
 - Applies security udpates with the driver and firmware updates
 - Places the latest hotfixes and the appliance verification utility (PAV) on your appliance so they are ready to install with no need to download them.


<!--MSDN references-->
[database compatibility level 130]:https://msdn.microsoft.com/library/bb510680.aspx
[Column-level SQL collations]:https://msdn.microsoft.com/library/ms143726.aspx
[Nonclustered indexes on clustered columnstore indexes]:https://msdn.microsoft.com/library/ms188783.aspx
[VARCHAR(MAX)]:https://msdn.microsoft.com/library/ms176089.aspx
[NVARCHAR(MAX)]:https://msdn.microsoft.com/library/ms186939.aspx
[VARBINARY(MAX)]:https://msdn.microsoft.com/library/ms188362.aspx
[SYSNAME]:https://msdn.microsoft.com/library/ms188021.aspx
[SELECT...INTO]:https://msdn.microsoft.com/library/ms188029.aspx
[sp_spaceused()]:https://msdn.microsoft.com/library/ms188776.aspx
[Wide tables]:https://msdn.microsoft.com/library/ms143432.aspx
[BULK INSERT]:https://msdn.microsoft.com/library/ms188365.aspx
[bcp Utility]:https://msdn.microsoft.com/library/ms162802.aspx
[UNIQUEIDENTIFIER]:https://msdn.microsoft.com/library/ms187942.aspx
[NUMERIC]:https://msdn.microsoft.com/library/ms187746.aspx
[ROWS or RANGE]:https://msdn.microsoft.com/library/ms189461.aspx
[FIRST_VALUE]:https://msdn.microsoft.com/library/hh213018.aspx
[LAST_VALUE]:https://msdn.microsoft.com/library/hh231517.aspx
[CUME_DIST]:https://msdn.microsoft.com//library/hh231078.aspx
[PERCENT_RANK]:https://msdn.microsoft.com/library/hh213573.aspx
[CHECKSUM()]:https://msdn.microsoft.com/library/ms189788.aspx
[BINARY_CHECKSUM()]:https://msdn.microsoft.com/library/ms173784.aspx
[HAS_PERMS_BY_NAME()]:https://msdn.microsoft.com/library/ms189802.aspx
[NEWID()]:https://msdn.microsoft.com/library/ms190348.aspx
[RAND()]:https://msdn.microsoft.com/library/ms177610.aspx


  

  


