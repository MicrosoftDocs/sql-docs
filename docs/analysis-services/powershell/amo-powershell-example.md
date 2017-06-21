---
title: "AMO PowerShell Example | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "reference"
ms.assetid: 1e06d114-d5b9-486b-a046-4078c9c253b1
caps.latest.revision: 8
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# AMO PowerShell Example

[!INCLUDE[ssas-appliesto-sqlas-all-aas](../../includes/ssas-appliesto-sqlas-all-aas.md)]

>[!NOTE] This article may contain outdated information and examples.  
>

  Below is an example PowerShell script that calls Analysis Services Management Object (AMO) types to create a Tabular database (compatibility level 1200 or higher) based on a SQL Server relational database.  
  
 This example is for demonstration purposes only. It stops short of defining table relationships, and it does not include data import.  This makes the script more of a snippet than execution-ready code. To be runnable, it would need other helper functions and more commands  to fill in the gaps.  
  
## DatabaseGenerate.ps1 Example  
  
```  
#  
# DatabaseGeneration.ps1  
# This scripts automates the creation of an SSAS Tabular Database from a source SQL Server.  
# This requires AMO and SMO as installed components in the GAC  
#  
# The core functionality is exposed by the Generate-ASDatabase cmdlet.  
#   
# Sample Command:  
# Generate-ASDatabases "SERVER_01\TB_Instance" "SQLCLDB4" "AS_AdventureWorksDW2012" 3 100  
#   
  
function Generate-ASDatabases()  
{  
    param([string] $ASServerInstance, [string] $RelationalInstance, [string] $databaseName, [int]$dbCount, [int] $tableCount)  
  
    [System.Reflection.Assembly]::LoadWithPartialName("Microsoft.SqlServer.Smo")  
    [System.Reflection.Assembly]::LoadWithPartialName("Microsoft.SqlServer.Management.Sdk.Sfc")  
    [System.Reflection.Assembly]::LoadWithPartialName("Microsoft.AnalysisServices.Tabular")  
    [System.Reflection.Assembly]::LoadWithPartialName("Microsoft.AnalysisServices.Core")  
  
    for ($db = 0; $db -lt $dbCount; $db++)  
    {  
        #  Sample:  
        #  DB Instance: SQLCLDB4  
        #  DatabaseName: AS_AdventureWorksDW2012  
        #  
        "Creating Database $db" | Out-Host  
        Generate-SingleDatabase $RelationalInstance $databaseName $ASServerInstance $tableCount   
    }  
}  
  
function Generate-SingleDatabase()  
{  
    param([string]$DBInstance, [string]$DatabaseName, [string]$ASInstance, [int]$TableCount)  
  
    $dbServer = New-Object -TypeName Microsoft.SqlServer.Management.Smo.Server -ArgumentList $DBInstance;  
  
    [Microsoft.SqlServer.Management.Smo.Database]$relDb;  
  
    foreach ($rDb in $dbServer.Databases)  
    {  
        $tempDb = [Microsoft.SqlServer.Management.Smo.Database]$rDb;  
        if ($tempDb.Name -eq $DatabaseName)  
        {  
            $relDb = $tempDb;  
            break;  
        }  
    }  
  
    if ($relDb -eq $nul)  
    {  
        return;  
    }  
  
    #Create an AS Database based on the SQL Server with the specified number of Tables   
    $asServer = New-Object -TypeName Microsoft.AnalysisServices.Tabular.Server;  
    #region Create the database / model / datasource  
  
    $connString = "Provider=MSOLAP;DataSource=" + $ASInstance + ";SQLQueryMode=DataKeys";  
    $asServer.Connect($connString);  
  
    $newDatabaseName = $asServer.Databases.GetNewName("TomDb_T" + $TableCount);  
  
    $asDb = Create-ASDatabase $asServer $newDatabaseName;  
  
    $model = Create-ASModel $relDb;  
  
    $tomDataSource = Add-ASDataSourceToModel $model $DBInstance $DatabaseName;  
  
    $asDb.Model = $model;  
  
    $asDb.Update([Microsoft.AnalysisServices.UpdateOptions]::ExpandFull);  
  
    $tomDataSource = $model.DataSources[0];  
  
    $tables = 0;  
    $tableSuffix = 0;  
  
    while ($tables -lt $TableCount)  
    {  
        #Set up the tables in the model based on tables in the relational database.  
  
        foreach ($dbTable in $relDb.Tables)  
        {  
            $relTable = [Microsoft.SqlServer.Management.Smo.Table]$dbTable;  
            if ($tables -ge $TableCount)  
            {  
                break;  
            }  
            $t = Create-ASTable $relTable $tableSuffix;  
  
            Create-ASPartition $tomDataSource $relTable $t;  
  
            Add-ASColumns $relTable $t;  
  
            if ($t.Columns.Count -gt 0)  
            {  
                $model.Tables.Add($t);  
            }  
  
            $tables++;  
        }  
        $tableSuffix++;  
    }  
    $model.SaveChanges();  
}  
  
function Add-ASColumns  
{  
    param([Microsoft.SqlServer.Management.Smo.Table] $relTable, [Microsoft.AnalysisServices.Tabular.Table] $t)  
  
    $columns = $relTable.Columns;  
  
    foreach ($rc in $columns)  
    {  
        $relColumn = [Microsoft.SqlServer.Management.Smo.Column]$rc;  
  
        $type = Get-TabularColumnTypeFromSQL $relColumn.DataType.SqlDataType;  
        if ($type -eq [Microsoft.AnalysisServices.Tabular.DataType]::Unknown)  
        {  
            continue;  
        }  
  
        $col = New-Object -TypeName Microsoft.AnalysisServices.Tabular.DataColumn;  
        $col.Name = $relColumn.Name;  
        $col.SourceColumn = $relColumn.Name;  
        $col.SourceProviderType = $relColumn.DataType.ToString();  
        $col.DataType = $type;  
  
        $t.Columns.Add($col);  
    }  
}  
  
function Create-ASPartition()  
{  
    param([Microsoft.AnalysisServices.Tabular.DataSource] $tomDataSource, [Microsoft.SqlServer.Management.Smo.Table] $relTable, [Microsoft.AnalysisServices.Tabular.Table] $t)  
  
    $partition = New-Object -TypeName Microsoft.AnalysisServices.Tabular.Partition;  
    $partition.Source = New-Object -TypeName Microsoft.AnalysisServices.Tabular.QueryPartitionSource;  
    $partition.Source.DataSource = $tomDataSource;  
    $partition.Source.Query = "SELECT * FROM " + $relTable.Name;  
    $partition.Name = $t.Name;  
    $partition.Mode = [Microsoft.AnalysisServices.Tabular.ModeType]::Default;  
    $partition.DataView = [Microsoft.AnalysisServices.Tabular.DataViewType]::Full;  
  
    $t.Partitions.Add($partition);  
}  
  
function Create-ASTable()  
{  
    param([Microsoft.SqlServer.Management.Smo.Table] $relTable, [int] $tableSuffix)  
  
    $tableName = $relTable.Name;  
    if ($tableSuffix -gt 0)  
    {  
        $tableName = $tableName + $tableSuffix;  
    }  
  
    $t = New-Object -TypeName Microsoft.AnalysisServices.Tabular.Table;  
    $t.Name = $tableName;  
    $t.Description = ("Auto Table: " + $relTable.Name);  
    $t.IsHidden = $false;  
    $t.DataCategory = $nul;  
  
    return $t;  
}  
  
function Create-ASDatabase()  
{  
    param([Microsoft.AnalysisServices.Tabular.Server] $asServer, [string] $dbName)  
  
    $asDb = [Microsoft.AnalysisServices.Tabular.Database] ( New-Object -TypeName Microsoft.AnalysisServices.Tabular.Database -ArgumentList $dbName );  
  
    $asDb.Description = "Automatically created Database";  
    $asDb.StorageEngineUsed = [Microsoft.AnalysisServices.StorageEngineUsed]::TabularMetadata;  
    $asDb.Language = 0;  
    $asDb.CompatibilityLevel = 1200;  
    $asDb.Annotations.Add("ClientCompatibilityLevel", "400") | Out-Null;  
    $asDb.Language = 1033;  
  
    $asServer.Databases.Add($asDb) | Out-Null;  
  
    $asDb.Update() | Out-Null  
  
    return $asDb;  
}  
  
function Create-ASModel([Microsoft.SqlServer.Management.Smo.Database] $relDb)  
{  
    $model = New-Object -TypeName Microsoft.AnalysisServices.Tabular.Model;  
    $model.Name = "Model";  
    $model.Culture = ([System.Globalization.CultureInfo]::CurrentUICulture).ToString();  
    $model.Description = "Auto Model";  
    return $model;  
}  
  
function Add-ASDataSourceToModel([Microsoft.AnalysisServices.Tabular.Model] $model, [string] $DBInstance, [string] $DatabaseName)  
{  
    $tomDataSource = New-Object -TypeName Microsoft.AnalysisServices.Tabular.ProviderDataSource;  
  
    $tomDataSource.ConnectionString = "DataSource=" + $DBInstance + "Initial Catalog=" + $DatabaseName;  
    $tomDataSource.Description = "Automatically created data source";  
    $tomDataSource.ImpersonationMode = [Microsoft.AnalysisServices.Tabular.ImpersonationMode]::ImpersonateServiceAccount;  
    $tomDataSource.Isolation = [Microsoft.AnalysisServices.Tabular.DatasourceIsolation]::ReadCommitted;  
    $tomDataSource.MaxConnections = 100;  
    $tomDataSource.Name = "SQLCLAS4 Data Source";  
    $tomDataSource.Provider = "SQLNCLI11";  
    $tomDataSource.Timeout = 1800;  
  
    $model.DataSources.Add($tomDataSource);  
  
    return $tomDataSource;  
}  
  
function Get-TabularColumnTypeFromSQL()  
{  
    param([Microsoft.SqlServer.Management.Smo.SqlDataType] $columnType)  
  
    switch ($columnType)  
    {  
        Money { return [Microsoft.AnalysisServices.Tabular.DataType]::Decimal }  
        SmallMoney { return [Microsoft.AnalysisServices.Tabular.DataType]::Decimal }  
        Decimal { return [Microsoft.AnalysisServices.Tabular.DataType]::Decimal }  
  
        TinyInt { return [Microsoft.AnalysisServices.Tabular.DataType]::Int64 }  
        SmallInt { return [Microsoft.AnalysisServices.Tabular.DataType]::Int64 }  
        Int { return [Microsoft.AnalysisServices.Tabular.DataType]::Int64 }  
        BigInt { return [Microsoft.AnalysisServices.Tabular.DataType]::Int64 }  
        Numeric { return [Microsoft.AnalysisServices.Tabular.DataType]::Int64 }  
  
        Bit { return [Microsoft.AnalysisServices.Tabular.DataType]::Boolean }  
  
        Date { return [Microsoft.AnalysisServices.Tabular.DataType]::DateTime }  
        DateTime { return [Microsoft.AnalysisServices.Tabular.DataType]::DateTime }  
        DateTime2 { return [Microsoft.AnalysisServices.Tabular.DataType]::DateTime }  
        DateTimeOffset { return [Microsoft.AnalysisServices.Tabular.DataType]::DateTime }  
        SmallDateTime { return [Microsoft.AnalysisServices.Tabular.DataType]::DateTime }  
        Timestamp { return [Microsoft.AnalysisServices.Tabular.DataType]::DateTime }  
  
        Float { return [Microsoft.AnalysisServices.Tabular.DataType]::Double }  
        Real { return [Microsoft.AnalysisServices.Tabular.DataType]::Double }  
  
        Text { return [Microsoft.AnalysisServices.Tabular.DataType]::String }  
        NVarCharMax { return [Microsoft.AnalysisServices.Tabular.DataType]::String }  
        NVarChar { return [Microsoft.AnalysisServices.Tabular.DataType]::String }  
        NText { return [Microsoft.AnalysisServices.Tabular.DataType]::String }  
        NChar { return [Microsoft.AnalysisServices.Tabular.DataType]::String }  
        Char { return [Microsoft.AnalysisServices.Tabular.DataType]::String }  
  
        Variant { return [Microsoft.AnalysisServices.Tabular.DataType]::Unknown }  
        default { return [Microsoft.AnalysisServices.Tabular.DataType]::Unknown }  
    }  
}  
```  
  
  
  