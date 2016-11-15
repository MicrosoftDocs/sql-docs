---
# required metadata

title: Configure SQL Server on Linux with mssql-conf - SQL Server vNext CTP1 | Microsoft Docs
description: This topic describes how to use the mssql-conf tool to  configure SQL Server vNext settings on Linux.
author: luisbosquez 
ms.author: lbosq 
manager: jhubbard
ms.date: 11/15/2016
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 06798dff-65c7-43e0-9ab3-ffb23374b322

# optional metadata

# keywords: ""
# ROBOTS: ""
# audience: ""
# ms.devlang: ""
# ms.reviewer: ""
# ms.suite: ""
# ms.tgt_pltfrm: ""
# ms.custom: ""
---
# Configure SQL Server on Linux with mssql-conf
**mssql-conf** is a configuration script that installs with SQL Server vNext CTP1 for both Red Hat Enterprise Linux and Ubuntu. You can use this utility to set the following parameters:

- [TCP port](#tcpport): Change the port where SQL Server will listen for connections.
- [Default data directory](#datadir): Change the directory where the new SQL Server database data files (.mdf).
- [Default log directory](#datadir): Changes the directory where the new SQL Server database log (.ldf) files are created.
- [Default dump directory](#dumpdir): Change the directory where SQL Server will deposit the memory dumps and other troubleshooting files by default.
- [Default backup directory](#backupdir): Change the directory where SQL Server will send the backup files by default. 
- [Set traceflags](#traceflags): Set the traceflags that the service is going to use.
- [Set collation](#collation): Set a new collation for SQL Server on Linux.

The following sections show examples of how to use mssql-conf for each of these scenarios.

## <a id="tcpport"></a> Change the TCP port

This option will let you change the TCP port where SQL Server will listen for connections. By default, this port is set to 1433. To change the port, run the following commands:

1. Run the mssql-conf script as root with the "set" command for "tcpport":

        $ sudo /opt/mssql/bin/mssql.conf set tcpport <new_tcp_port>

2. Restart the SQL Server service as instructed by the configuration utility:

        $ sudo systemctl restart mssql-server

3. When connecting to SQL Server now, you will need to specify the port with a comma (,) after the hostname or IP address. For example, to connect with SQLCMD, you would use the following command:

        $ sqlcmd -S localhost,<new_tcp_port> -U test -P test

## <a id="datadir"></a> Change the default data or log directory location

This option will let you change the location where the new database and log files are created. By default, this location is /var/opt/mssql/data. To achieve this, follow these steps:

1. Create the directory where the new database's data and log files will reside. For example, we will use /tmp/data:  

        $ sudo mkdir /tmp/data

2. Change the owner and group of the directory to the "mssql" user:

        $ sudo chown mssql /tmp/data
        $ sudo chgrp mssql /tmp/data

3. Use mssql-conf to change the default data directory with the "set" command:

        $ sudo /opt/mssql/bin/mssql-conf set defaultdatadir /tmp/data

4. Restart the SQL Server service as instructed by the configuration utility:

        $ sudo systemctl restart mssql-server

5. Troubleshooting: Move the tempdb data directories to this new location. The service will not start if it can't restore the tempdb database files. Move them to the new folder using the following commands:

        $ sudo mv /var/opt/mssql/data/tempdb.mdf /tmp/data
        $ sudo mv /var/opt/mssql/data/templog.ldf /tmp/data

6. Restart the SQL Server service again:

        $ sudo systemctl restart mssql-server

7. Now all the database files for the new databases created will be stored in this new location. If you would like to change the location of the log (.ldf) files of the new databases, you can use the following "set" command:

        $ sudo /opt/mssql/bin/mssql-conf set defaultlogdir /tmp/log

8. This command also assumes that a /tmp/log directory exists, and that it is under the user and group "mssql".

## <a id="dumpdir"></a> Change the default dump directory location

This option will let you change the default location where the memory and SQL dumps are generated whenever there is a crash. By default, these files are generated in /var/opt/mssql/log.

To set up this new location, use the following commands:

1. Create the directory where the dump files will reside. For example, we will use /tmp/dump:  

        $ sudo mkdir /tmp/dump

2. Change the owner and group of the directory to the "mssql" user:

        $ sudo chown mssql /tmp/dump
        $ sudo chgrp mssql /tmp/dump

3. Use mssql-conf to change the default data directory with the "set" command:

        $ sudo /opt/mssql/bin/mssql-conf set defaultdumpdir /tmp/dump

4. Restart the SQL Server service as instructed by the configuration utility:

        $ sudo systemctl restart mssql-server

## <a id="backupdir"></a> Change the default backup directory location

This option will let you change the default location where the backup files are generaqted. By default, these files are generated in /var/opt/mssql/data.

To set up this new location, use the following commands:

1. Create the directory where the backup files will reside. For example, we will use /tmp/backup:  

        $ sudo mkdir /tmp/backup

2. Change the owner and group of the directory to the "mssql" user:

        $ sudo chown mssql /tmp/backup
        $ sudo chgrp mssql /tmp/backup

3. Use mssql-conf to change the default backup directory with the "set" command:

        $ sudo /opt/mssql/bin/mssql-conf set defaultbackupdir /tmp/backup

4. Restart the SQL Server service as instructed by the configuration utility:

        $ sudo systemctl restart mssql-server

## <a id="traceflags"></a> Enable/Disable traceflags

This option will let you enable or disable traceflags for the startup of the SQL Server service. To enable/disable a traceflag use the following commands:

1. Enable a traceflag using the following command. For example, for Traceflag 1234:  

        $ sudo /opt/mssql/bin/mssql-conf traceflag 1234 on

2. You can enable multiple traceflags by specifying them separately:

        $ sudo /opt/mssql/bin/mssql-conf traceflag 2345 3456 on

3. In a similar way, you can disable one or more enabled traceflags by specifying them and adding the "off" parameter:

        $ sudo /opt/mssql/bin/mssql-conf traceflag 1234 2345 3456 off

4. Restart the SQL Server service as instructed by the configuration utility for the changes to apply:

        $ sudo systemctl restart mssql-server

## <a id="collation"></a> Change the SQL Server collation

This option will let you change the collation value to any of the supported collations:


1. Run the "set-collation" option and follow the prompts:

        $ sudo /opt/mssql/bin/mssql-conf set-collation

2. The mssql-conf utility will try to restore the databases using the specified collation and restart the service. If there are any errors, it will roll-back the collation to the previous value.

The following is a list of supported collations:
        
-  Albanian_BIN
-  Arabic_BIN
-  Chinese_PRC_BIN
-  Chinese_PRC_CI_AS
-  Chinese_PRC_CS_AS
-  Chinese_Taiwan_Stroke_BIN
-  Chinese_Taiwan_Stroke_CI_AS
-  Chinese_Taiwan_Stroke_CS_AS
-  Cyrillic_General_BIN
-  Czech_BIN
-  Danish_Norwegian_CS_AS
-  Finnish_Swedish_CS_AS
-  Greek_BIN
-  Hebrew_BIN
-  Hungarian_BIN
-  Icelandic_CS_AS
-  Japanese_BIN
-  Japanese_CI_AS
-  Japanese_CS_AS
-  Korean_Wansung_BIN
-  Korean_Wansung_CI_AS
-  Korean_Wansung_CS_AS
-  Latin1_General_BIN
-  Latin1_General_CI_AS
-  Latin1_General_CS_AS
-  Macedonian_FYROM_90_BIN
-  SQL_1Xcompat_CP850_CI_AS
-  SQL_AltDiction_Cp1253_CS_AS
-  SQL_AltDiction_Cp850_CI_AI
-  SQL_AltDiction_Cp850_CI_AS
-  SQL_AltDiction_Cp850_CS_AS
-  SQL_AltDiction_Pref_CP850_CI_AS
-  SQL_Croatian_Cp1250_CI_AS
-  SQL_Croatian_Cp1250_CS_AS
-  SQL_Czech_Cp1250_CI_AS
-  SQL_Czech_Cp1250_CS_AS
-  SQL_Danish_Pref_Cp1_CI_AS
-  SQL_EBCDIC037_CP1_CS_AS
-  SQL_EBCDIC273_CP1_CS_AS
-  SQL_EBCDIC277_CP1_CS_AS
-  SQL_EBCDIC278_CP1_CS_AS
-  SQL_EBCDIC280_CP1_CS_AS
-  SQL_EBCDIC284_CP1_CS_AS
-  SQL_EBCDIC285_CP1_CS_AS
-  SQL_EBCDIC297_CP1_CS_AS
-  SQL_Estonian_Cp1257_CI_AS
-  SQL_Estonian_Cp1257_CS_AS
-  SQL_Hungarian_Cp1250_CI_AS
-  SQL_Hungarian_Cp1250_CS_AS
-  SQL_Icelandic_Pref_Cp1_CI_AS
-  SQL_Latin1_General_Cp1250_CI_AS
-  SQL_Latin1_General_Cp1250_CS_AS
-  SQL_Latin1_General_Cp1251_CI_AS
-  SQL_Latin1_General_Cp1251_CS_AS
-  SQL_Latin1_General_Cp1253_CI_AI
-  SQL_Latin1_General_Cp1253_CI_AS
-  SQL_Latin1_General_Cp1253_CS_AS
-  SQL_Latin1_General_Cp1254_CI_AS
-  SQL_Latin1_General_Cp1254_CS_AS
-  SQL_Latin1_General_Cp1255_CI_AS
-  SQL_Latin1_General_Cp1255_CS_AS
-  SQL_Latin1_General_Cp1256_CI_AS
-  SQL_Latin1_General_Cp1256_CS_AS
-  SQL_Latin1_General_Cp1257_CI_AS
-  SQL_Latin1_General_Cp1257_CS_AS
-  SQL_Latin1_General_Cp1_CI_AI
-  SQL_Latin1_General_Cp1_CI_AS
-  SQL_Latin1_General_Cp1_CS_AS
-  SQL_Latin1_General_Cp437_BIN
-  SQL_Latin1_General_Cp437_CI_AI
-  SQL_Latin1_General_Cp437_CI_AS
-  SQL_Latin1_General_Cp437_CS_AS
-  SQL_Latin1_General_Cp850_BIN
-  SQL_Latin1_General_Cp850_CI_AI
-  SQL_Latin1_General_Cp850_CI_AS
-  SQL_Latin1_General_Cp850_CS_AS
-  SQL_Latin1_General_Pref_CP1_CI_AS
-  SQL_Latin1_General_Pref_CP437_CI_AS
-  SQL_Latin1_General_Pref_CP850_CI_AS
-  SQL_Latvian_Cp1257_CI_AS
-  SQL_Latvian_Cp1257_CS_AS
-  SQL_Lithuanian_Cp1257_CI_AS
-  SQL_Lithuanian_Cp1257_CS_AS
-  SQL_MixDiction_Cp1253_CS_AS
-  SQL_Polish_Cp1250_CI_AS
-  SQL_Polish_Cp1250_CS_AS
-  SQL_Romanian_Cp1250_CI_AS
-  SQL_Romanian_Cp1250_CS_AS
-  SQL_Scandinavian_Cp850_CI_AS
-  SQL_Scandinavian_Cp850_CS_AS
-  SQL_Scandinavian_Pref_Cp850_CI_AS
-  SQL_Slovak_Cp1250_CI_AS
-  SQL_Slovak_Cp1250_CS_AS
-  SQL_Slovenian_Cp1250_CI_AS
-  SQL_Slovenian_Cp1250_CS_AS
-  SQL_SwedishPhone_Pref_Cp1_CI_AS
-  SQL_SwedishStd_Pref_Cp1_CI_AS
-  SQL_Ukrainian_Cp1251_CI_AS
-  SQL_Ukrainian_Cp1251_CS_AS
-  Thai_BIN
-  Thai_CI_AS
-  Thai_CS_AS
-  Turkish_BIN
-  Ukrainian_BIN

# Next steps

For other management tools and scenarios, see [Manage SQL Server on Linux](sql-server-linux-management-overview.md).