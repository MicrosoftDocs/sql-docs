---
title: Common schema collections
description: Describes all of the common schema collections supported by all of the .NET managed providers.
author: David-Engel
ms.author: v-davidengel
ms.date: 11/30/2020
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Common schema collections

[!INCLUDE[appliesto-netfx-netcore-netst-md](../../includes/appliesto-netfx-netcore-netst-md.md)]

[!INCLUDE[Driver_ADONET_Download](../../includes/driver_adonet_download.md)]

The common schema collections are the schema collections that are implemented by each of the .NET managed providers. You can query a .NET managed provider to determine the list of supported schema collections by calling the `GetSchema` method with no arguments, or with the schema collection name "MetaDataCollections". This method will return a <xref:System.Data.DataTable> with a list of the supported schema collections, the number of restrictions that they each support, and the number of identifier parts that they use. These collections describe all of the required columns. Providers are free to add more columns if they wish. For example, the Microsoft SqlClient Data Provider for SQL Server adds `ParameterName` to the restrictions collection.

If a provider is unable to determine the value of a required column, it will return null.

For more information about using the `GetSchema` methods, see [GetSchema and Schema Collections](getschema-and-schema-collections.md).

## MetaDataCollections

This collection exposes information about all of the schema collections supported by the provider that is currently used to connect to the database.

|ColumnName|DataType|Description|
|----------------|--------------|-----------------|
|CollectionName|`string`|The name of the collection to pass to the `GetSchema` method to return the collection.|
|NumberOfRestrictions|`int`|The number of restrictions that may be specified for the collection.|
|NumberOfIdentifierParts|`int`|The number of parts in the composite identifier/database object name. For example, in SQL Server, the value would be 3 for tables and 4 for columns.|

## DataSourceInformation

This schema collection exposes information about data source that the Microsoft SqlClient Data Provider for SQL Server is currently connected to.

|ColumnName|DataType|Description|
|----------------|--------------|-----------------|
|CompositeIdentifierSeparatorPattern|`string`|The regular expression to match the composite separators in a composite identifier. For example, `\.` (for SQL Server).<br /><br /> A composite identifier is typically what is used for a database object name, for example: `pubs.dbo.authors` or `pubs@dbo.authors`.<br /><br /> For SQL Server, use the regular expression `\.`. |
|DataSourceProductName|`string`|The name of the product accessed by the provider, such as "SQLServer".|
|DataSourceProductVersion|`string`|Indicates the version of the product accessed by the provider, in the data sources native format and not in Microsoft format.<br /><br /> In some cases, `DataSourceProductVersion` and `DataSourceProductVersionNormalized` will be the same value. |
|DataSourceProductVersionNormalized|`string`|A normalized version for the data source, such that it can be compared with `String.Compare()`. This format is consistent for all versions of the provider to prevent version 10 from sorting between version 1 and version 2.<br /><br /> For example, SQL Server uses the typical Microsoft `nn.nn.nnnn` format.<br /><br /> In some cases, `DataSourceProductVersion` and `DataSourceProductVersionNormalized` will be the same value. |
|GroupByBehavior|<xref:System.Data.Common.GroupByBehavior>|Specifies the relationship between the columns in a GROUP BY clause and the non-aggregated columns in the select list.|
|IdentifierPattern|`string`|A regular expression that matches an identifier and has a match value of the identifier. For example, `[A-Za-z0-9_#$]`.|
|IdentifierCase|<xref:System.Data.Common.IdentifierCase>|Indicates whether non-quoted identifiers are treated as case sensitive or not.|
|OrderByColumnsInSelect|`bool`|Specifies whether columns in an ORDER BY clause must be in the select list. A value of `true` indicates that they're required to be in the select list. A value of `false` indicates that they're not required to be in the select list.|
|ParameterMarkerFormat|`string`|A format string that represents how to format a parameter.<br /><br /> If named parameters are supported by the data source, the first placeholder in this string should be where the parameter name should be formatted.<br /><br /> For example, if the data source expects parameters to be named and prefixed with a `:` this string would be `:{0}`. When formatting this parameter with a name of `p1` the resulting string is `:p1`.<br /><br /> If the data source expects parameters to be prefixed with the `@`, but the names already include them, this string would be `{0}`, and the result of formatting a parameter named `@p1` would be `@p1`.<br /><br /> For data sources that don't expect named parameters and expect the use of the `?` character, the format string can be specified as `?`, which would ignore the parameter name. |
|ParameterMarkerPattern|`string`|A regular expression that matches a parameter marker. It will have a match value of the parameter name, if any.<br /><br /> For example, if named parameters are supported with an `@` lead-in character that will be included in the parameter name, this pattern would be: `(\@[A-Za-z0-9_$#]*)`.<br /><br /> However, if named parameters are supported with a `:` as the lead-in character and it isn't part of the parameter name, this pattern would be: `:([A-Za-z0-9_$#]\*)`.<br /><br /> If the data source doesn't support named parameters, this pattern would be `?`.|
|ParameterNameMaxLength|`int`|The maximum length of a parameter name in characters. Visual Studio expects that if parameter names are supported, the minimum value for the maximum length is 30 characters.<br /><br /> If the data source doesn't support named parameters, this property returns zero.|
|ParameterNamePattern|`string`|A regular expression that matches the valid parameter names. Different data sources have different rules about the characters that may be used for parameter names.<br /><br /> Visual Studio expects that if parameter names are supported, the characters `\p{Lu}\p{Ll}\p{Lt}\p{Lm}\p{Lo}\p{Nl}\p{Nd}` are the minimum supported set of characters that are valid for parameter names.|
|QuotedIdentifierPattern|`string`|A regular expression that matches a quoted identifier and has a match value of the identifier itself without the quotes. For example, if the data source used double-quotes to identify quoted identifiers, this pattern would be: `(([^\\"]\|\\"\\")*)`.|
|QuotedIdentifierCase|<xref:System.Data.Common.IdentifierCase>|Indicates whether quoted identifiers are treated as case sensitive or not.|
|StatementSeparatorPattern|`string`|A regular expression that matches the statement separator.|
|StringLiteralPattern|`string`|A regular expression that matches a string literal and has a match value of the literal itself. For example, if the data source used single-quotes to identify strings, this pattern would be: `('([^']\|'')*')`'|
|SupportedJoinOperators|<xref:System.Data.Common.SupportedJoinOperators>|Specifies what types of SQL join statements are supported by the data source.|

## DataTypes

This schema collection exposes information about the data types that are supported by the database that the provider is currently connected to.

|ColumnName|DataType|Description|
|----------------|--------------|-----------------|
|TypeName|`string`|The provider-specific data type name.|
|ProviderDbType|`int`|The provider-specific type value that should be used when specifying a parameter's type. For example, `SqlDbType.Money`.|
|ColumnSize|`long`|The length of a non-numeric column or parameter. This value refers to either the maximum or the length defined for this type by the provider.<br /><br /> For character data, this value is the maximum or defined length in units, defined by the data source. <br /><br /> For date-time data types, this value is the length of the string representation (assuming the maximum allowed precision of the fractional seconds component).<br /><br /> If the data type is numeric, this value is the upper bound on the maximum precision of the data type.|
|CreateFormat|`string`|Format string that represents how to add this column to a data definition statement, such as CREATE TABLE. Each element in the `CreateParameter` array should be represented by a "parameter marker" in the format string.<br /><br /> For example, the SQL data type DECIMAL needs a precision and a scale. In this case, the format string would be `DECIMAL({0},{1})`.|
|CreateParameters|`string`|The creation parameters that must be specified when creating a column of this data type. Each creation parameter is listed in the string, separated by a comma in the order they're to be supplied.<br /><br /> For example, the SQL data type DECIMAL needs a precision and a scale. In this case, the creation parameters should contain the string "precision, scale".<br /><br /> In a text command to create a DECIMAL column with a precision of 10 and a scale of 2, the value of the CreateFormat column might be `DECIMAL({0},{1})` and the complete type specification would be `DECIMAL(10,2)`.|
|DataType|`string`|The name of the .NET type of the data type.|
|IsAutoincrementable|`bool`|`true` - Values of this data type may be auto-incrementing.<br /><br /> `false` - Values of this data type may not be auto-incrementing.<br /><br /> This property merely indicates whether a column of this data type may be auto-incrementing, not that all columns of this type are auto-incrementing.|
|IsBestMatch|`bool`|`true` - The data type is the best match between all data types in the data store and the .NET data type indicated by the value in the `DataType` column.<br /><br /> `false` - The data type isn't the best match.<br /><br /> For each set of rows in which the value of the `DataType` column is the same, the `IsBestMatch` column is set to `true` in only one row.|
|IsCaseSensitive|`bool`|`true` - The data type is a character type and is case-sensitive.<br /><br /> `false` - The data type isn't a character type or isn't case-sensitive.|
|IsFixedLength|`bool`|`true` - Columns of this data type created by the data definition language (DDL) will be of fixed length.<br /><br /> `false` - Columns of this data type created by the DDL will be of variable length.<br /><br /> `DBNull.Value` - It isn't known whether the provider will map this field with a fixed-length or variable-length column.|
|IsFixedPrecisionScale|`bool`|`true` - The data type has a fixed precision and scale.<br /><br /> `false` - The data type doesn't have a fixed precision and scale.|
|IsLong|`bool`|`true` - The data type contains very long data; the definition of very long data is provider-specific.<br /><br /> `false` - The data type doesn't contain very long data.|
|IsNullable|`bool`|`true` - The data type is nullable.<br /><br /> `false` - The data type isn't nullable.<br /><br /> `DBNull.Value` - It isn't known whether the data type is nullable.|
|IsSearchable|`bool`|`true` - The data type can be used in a WHERE clause with any operator except the LIKE predicate.<br /><br /> `false` - The data type cannot be used in a WHERE clause with any operator except the LIKE predicate.|
|IsSearchableWithLike|`bool`|`true` - The data type can be used with the LIKE predicate.<br /><br /> `false` - The data type cannot be used with the LIKE predicate.|
|IsUnsigned|`bool`|`true` - The data type is unsigned.<br /><br /> `false` - The data type is signed.<br /><br /> `DBNull.Value` - Not applicable to data type.|
|MaximumScale|`short`|If the type indicator is a numeric type, this value is the maximum number of digits allowed to the right of the decimal point. Otherwise, this value is `DBNull.Value`.|
|MinimumScale|`short`|If the type indicator is a numeric type, this value is the minimum number of digits allowed to the right of the decimal point. Otherwise, this value is `DBNull.Value`.|
|IsConcurrencyType|`bool`|`true` – The data type is updated by the database every time the row is changed and the value of the column is different from all previous values.<br /><br /> `false` – The data type is note updated by the database every time the row is changed.<br /><br /> `DBNull.Value` – The database doesn't support this data type.|
|IsLiteralSupported|`bool`|`true` – The data type can be expressed as a literal.<br /><br /> `false` – The data type can't be expressed as a literal.|
|LiteralPrefix|`string`|The prefix applied to a given literal.|
|LiteralSuffix|`string`|The suffix applied to a given literal.|

## Restrictions

This schema collection exposed information about the restrictions that are supported by the provider that is currently used to connect to the database.

|ColumnName|DataType|Description|
|----------------|--------------|-----------------|
|CollectionName|`string`|The name of the collection that these restrictions apply to.|
|RestrictionName|`string`|The name of the restriction in the collection.|
|RestrictionDefault|`string`|Ignored.|
|RestrictionNumber|`int`|The actual location in the collections restrictions that this particular restriction falls in.|

## ReservedWords

This schema collection exposes information about the words that are reserved by the database that the provider that is currently connected to.

|ColumnName|DataType|Description|
|----------------|--------------|-----------------|
|ReservedWord|`string`|Provider specific reserved word.|

## See also

- [Retrieving database schema information](retrieving-database-schema-information.md)
- [GetSchema and schema collections](getschema-and-schema-collections.md)
- [Microsoft ADO.NET for SQL Server](microsoft-ado-net-sql-server.md)
