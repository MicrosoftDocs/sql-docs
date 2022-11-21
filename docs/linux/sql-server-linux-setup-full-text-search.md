---
title: Install SQL Server Full-Text Search on Linux
description: Learn how to install SQL Server Full-Text Search on Linux. Full-Text Search enables you to run full-text queries against character-based data in SQL Server tables.
author: VanMSFT
ms.author: vanto
ms.date: 10/02/2017
ms.topic: conceptual
ms.service: sql
ms.subservice: linux
ms.assetid: bb42076f-e823-4cee-9281-cd3f83ae42f5
ms.custom:
  - intro-installation
---
# Install SQL Server Full-Text Search on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

The following steps install [SQL Server Full-Text Search](../relational-databases/search/full-text-search.md) (**mssql-server-fts**) on Linux. Full-Text Search enables you to run full-text queries against character-based data in SQL Server tables. For known issues for this release, see the [Release Notes](sql-server-linux-release-notes-2017.md).

> [!NOTE]
> Before installing SQL Server Full-Text Search, first [install SQL Server](sql-server-linux-setup.md#platforms). This configures the keys and repositories that you use when installing the **mssql-server-fts** package.

Install SQL Server Full-Text Search for your platform:

- [Red Hat Enterprise Linux](#RHEL)
- [Ubuntu](#ubuntu)
- [SUSE Linux Enterprise Server](#SLES)

## <a name="RHEL">Install on RHEL</a>

Use the following commands to install the **mssql-server-fts** on Red Hat Enterprise Linux. 

```bash
sudo yum install -y mssql-server-fts
```

If you already have **mssql-server-fts** installed, you can update to the latest version with the following commands:

```bash
sudo yum check-update
sudo yum update mssql-server-fts
```

If you need an offline installation, locate the Full-text Search package download in the [Release notes](sql-server-linux-release-notes-2017.md). Then use the same offline installation steps described in the article [Install SQL Server](sql-server-linux-setup.md#offline).

## <a name="ubuntu">Install on Ubuntu</a>

Use the following commands to install the **mssql-server-fts** on Ubuntu. 

```bash
sudo apt-get update 
sudo apt-get install -y mssql-server-fts
```

If you already have **mssql-server-fts** installed, you can update to the latest version with the following commands:

```bash
sudo apt-get update 
sudo apt-get install -y mssql-server-fts 
```

If you need an offline installation, locate the Full-text Search package download in the [Release notes](sql-server-linux-release-notes-2017.md). Then use the same offline installation steps described in the article [Install SQL Server](sql-server-linux-setup.md#offline).

## <a name="SLES">Install on SLES</a>

Use the following commands to install the **mssql-server-fts** on SUSE Linux Enterprise Server. 

```bash
sudo zypper install mssql-server-fts
```

If you already have **mssql-server-fts** installed, you can update to the latest version with the following commands:

```bash
sudo zypper refresh
sudo zypper update mssql-server-fts
```

If you need an offline installation, locate the Full-text Search package download in the [Release notes](sql-server-linux-release-notes-2017.md). Then use the same offline installation steps described in the article [Install SQL Server](sql-server-linux-setup.md#offline).

## Supported languages

Full-Text Search uses [word breakers](../relational-databases/search/configure-and-manage-word-breakers-and-stemmers-for-search.md) that determine how to identify individual words based on language. You can get a list of registered word breakers by querying the **sys.fulltext_languages** catalog view. Word breakers for the following languages are installed with SQL Server:

| Language | Language ID |
|---|---|
| Neutral | 0 |
| Arabic | 1025 |
| Bengali (India) | 1093 |
| Bokmål | 1044 |
| Portuguese (Brazil) | 1046 |
| British English | 2057 |
| Bulgarian | 1026 |
| Catalan | 1027 |
| Chinese (Hong Kong SAR, PRC) | 3076 |
| Chinese (Macao SAR) | 5124 |
| Chinese (Singapore) | 4100 |
| Croatian | 1050 |
| Czech | 1029 |
| Danish | 1030 |
| Dutch | 1043 |
| English | 1033 |
| French | 1036 |
| German | 1031 |
| Greek | 1032 |
| Gujarati | 1095 |
| Hebrew | 1037 |
| Hindi | 1081 |
| Icelandic | 1039 |
| Indonesian | 1057 |
| Italian | 1040 |
| Japanese | 1041 |
| Kannada | 1099 |
| Korean | 1042 |
| Latvian | 1062 |
| Lithuanian | 1063 |
| Malay - Malaysia | 1086 |
| Malayalam | 1100 |
| Marathi | 1102 |
| Polish | 1045 |
| Portuguese | 2070 |
| Punjabi | 1094 |
| Romanian | 1048 |
| Russian | 1049 |
| Serbian (Cyrillic) | 3098 |
| Serbian (Latin) | 2074 |
| Simplified Chinese | 2052 |
| Slovak | 1051 |
| Slovenian | 1060 |
| Spanish | 3082 |
| Swedish | 1053 |
| Tamil | 1097 |
| Telugu | 1098 |
| Thai | 1054 |
| Traditional Chinese | 1028 |
| Turkish | 1055 |
| Ukrainian | 1058 |
| Urdu | 1056 |
| Vietnamese | 1066 |

## <a id="filters"></a> Filters

Full-Text Search also works with text stored in binary files. But in this case, an installed filter is required to process the file. For more information about filters, see [Configure and Manage Filters for Search](../relational-databases/search/configure-and-manage-filters-for-search.md).

You can see a list of installed filters by calling **sp_help_fulltext_system_components 'filter'**. For SQL Server, the following filters are installed:

| Component Name | Class ID | Version |
|---|---|---|
|.a | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.ans | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.asc | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.ascx | E0CA5340-4534-11CF-B952-00AA0051FE20 | 12.0.6828.0 |
|.asm | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
|.asp | E0CA5340-4534-11CF-B952-00AA0051FE20 | 12.0.6828.0 |
|.aspx | E0CA5340-4534-11CF-B952-00AA0051FE20 | 12.0.6828.0 |
|.asx | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.bas | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.bat | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
|.bcp | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.c | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
|.cc | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.cls | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.cmd | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
|.cpp | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
|.cs | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.csa | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.css | E0CA5340-4534-11CF-B952-00AA0051FE20 | 12.0.6828.0 |
|.csv | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.cxx | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
|.dbs | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.def | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
|.dic | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
|.dos | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.dsp | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.dsw | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.ext | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.faq | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.fky | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.h | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
|.hhc | E0CA5340-4534-11CF-B952-00AA0051FE20 | 12.0.6828.0 |
|.hpp | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
|.hta | E0CA5340-4534-11CF-B952-00AA0051FE20 | 12.0.6828.0 |
|.htm | E0CA5340-4534-11CF-B952-00AA0051FE20 | 12.0.6828.0 |
|.html | E0CA5340-4534-11CF-B952-00AA0051FE20 | 12.0.6828.0 |
|.htt | E0CA5340-4534-11CF-B952-00AA0051FE20 | 12.0.6828.0 |
|.htw | E0CA5340-4534-11CF-B952-00AA0051FE20 | 12.0.6828.0 |
|.htx | E0CA5340-4534-11CF-B952-00AA0051FE20 | 12.0.6828.0 |
|.hxx | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
|.i | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.ibq | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
|.ics | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.idl | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
|.idq | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.inc | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
|.inf | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
|.ini | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
|.inl | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.inx | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
|.jav | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.java | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.js | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
|.kci | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.lgn | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.log | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
|.lst | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.m3u | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
|.mak | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.mk | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.odc | E0CA5340-4534-11CF-B952-00AA0051FE20 | 12.0.6828.0 |
|.odh | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.odl | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.pkgdef | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.pkgundef | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.pl | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
|.prc | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.rc | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
|.rc2 | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.rct | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.reg | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
|.rgs | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.rtf | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
|.rul | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.s | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.scc | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.shtm | E0CA5340-4534-11CF-B952-00AA0051FE20 | 12.0.6828.0 |
|.shtml | E0CA5340-4534-11CF-B952-00AA0051FE20 | 12.0.6828.0 |
|.snippet | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.sol | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.sor | E0CA5340-4534-11CF-B952-00AA0051FE20 | 12.0.6828.0 |
|.srf | E0CA5340-4534-11CF-B952-00AA0051FE20 | 12.0.6828.0 |
|.stm | E0CA5340-4534-11CF-B952-00AA0051FE20 | 12.0.6828.0 |
|.tab | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.tdl | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.tlh | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.tli | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.trg | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.txt | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
|.udf | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.udt | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.url | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
|.usr | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.vbs | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
|.viw | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.vsct | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.vsixlangpack | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.vsixmanifest | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.vspscc | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.vsscc | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.vssscc | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.wri | C1243CA0-BF96-11CD-B579-08002B30BFEB | 12.0.6828.0 |
|.wtx | C7310720-AC80-11D1-8DF3-00C04FB6EF4F | 12.0.6828.0 |
|.xml | 41B9BE05-B3AF-460C-BF0B-2CDD44A093B1 | 12.0.9735.0 |

## Semantic search
[Semantic Search](../relational-databases/search/semantic-search-sql-server.md) builds on the Full-Text Search feature to extract and index statistically relevant *key phrases*. This enables you to query the meaning within documents in your database. It also helps to identify documents that are similar.

In order to use Semantic Search, you must first restore the Semantic Language Statistics database to your machine.

1. Use a tool, such as [sqlcmd](sql-server-linux-setup-tools.md), to run the following Transact-SQL command on your Linux SQL Server instance. This command restores the Language Statistics database.

   ```sql
   RESTORE DATABASE [semanticsdb] FROM
   DISK = N'/opt/mssql/misc/semanticsdb.bak' WITH FILE = 1,
   MOVE N'semanticsdb' TO N'/var/opt/mssql/data/semanticsDB.mdf',
   MOVE N'semanticsdb_log' TO N'/var/opt/mssql/data/semanticsdb_log.ldf', NOUNLOAD, STATS = 5
   GO
   ```

   > [!NOTE]
   > If necessary, update the paths in the previous RESTORE command to adjust for your configuration.

1. Run the following Transact-SQL command to register the semantic language statistics database.

    ```sql
    EXEC sp_fulltext_semantic_register_language_statistics_db @dbname = N'semanticsdb';  
    GO
    ```

## Next steps

For information about Full-Text Search, see [SQL Server Full-Text Search](../relational-databases/search/full-text-search.md). 
