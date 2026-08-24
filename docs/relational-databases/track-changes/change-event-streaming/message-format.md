---
title: "JSON Message format - change event streaming"
description: "Describes the message format for change event streaming"
author: nzagorac-ms
ms.author: nzagorac
ms.reviewer: mathoma
ms.date: 08/11/2026
ms.service: sql
ms.topic: "reference"
ms.custom:
  - ignite-2025
monikerRange: "=sql-server-ver17 || =sql-server-linux-ver17 || =azuresqldb-current || =azuresqldb-mi-current || =fabric-sqldb"
---

# JSON message format - change event streaming
[!INCLUDE [sql25-sqldb-sqlmi-sqldbfabric](../../../includes/applies-to-version/sqlserver2025-asdb-asmi-fabricsqldb.md)]

This article describes the CloudEvents message format that streams to Azure Event Hubs or Fabric Eventstream when you use the [change event streaming (CES)](overview.md) feature in [!INCLUDE [sssql25-md](../../../includes/sssql25-md.md)], Azure SQL Database, Azure SQL Managed Instance, and SQL database in Microsoft Fabric.

[!INCLUDE [change-event-streaming-preview](../../../includes/change-event-streaming-preview.md)]

## Overview

Change event streaming emits events that follow the [CloudEvents](https://github.com/cloudevents/spec) specification, so you can easily integrate them with event-driven systems. All CES CloudEvents contain 11 attributes (fields). You can configure CES to serialize the entire CloudEvent, including the `data` attribute, as native JSON or Avro binary. Native JSON events don't contain Avro binary sections. In both serialization formats, the `data` attribute has a byte-array type. The bytes use JSON or Avro binary encoding according to the selected serialization format and follow the [CES data attribute Avro schema](#ces-data-attribute-avro-schema).

[!INCLUDE [change-event-streaming-amqp-deprecation](../../../includes/change-event-streaming-amqp-deprecation.md)]

## Related specifications and resources

When applicable, the descriptions in this section come from the [CloudEvent specification](https://github.com/cloudevents/spec/blob/v1.0.2/cloudevents/spec.md), which includes more details.  

## Attributes

- **`specversion`**:
  - Data type: String
  - Required CloudEvent attribute
  - The version of the CloudEvents specification that the event uses. This version enables the interpretation of the context.

- **`type`**
  - Data type: String
  - Required CloudEvent attribute
  - Contains a value that describes the type of event related to the originating occurrence. The format of this value is defined by the producer and might include information such as the version of the type. For more information, see [Versioning of CloudEvents](https://github.com/cloudevents/spec/blob/v1.0.2/cloudevents/primer.md).
  - For change event streaming events, the type is currently: `com.microsoft.SQL.CES.DML.V{n}`, where `{n}` indicates the version of Microsoft change event streaming DML event schema.
    - The current latest schema version is 1.

- **`source`**
  - Data type: String
  - Required CloudEvent attribute
  - Identifies the context in which an event happened. The combination of source and ID must be unique for each event. Currently, this field is always sent as `\/` in events streamed from SQL.

- **`id`**
  - Data type: String
  - Required CloudEvent attribute
  - Identifies the event. Producers must ensure that the combination of source and ID is unique for each distinct event. If a duplicate event is resent (for example, due to a network error) it could have the same ID. Consumers might assume that events with identical source and ID are duplicates.

- **`logicalid`**
  - Data type: String
  - Extension attribute
  - Shared logical IDs identify split messages (due to Event Hubs message size restrictions).

- **`time`**
  - Data type: Timestamp
  - Optional CloudEvent attribute
  - UTC timestamp of when the commit happened within a SQL transaction that originally triggers a streamed event.

- **`datacontenttype`**
  - Data type: String
  - Optional CloudEvent attribute
  - Content type of data value. This attribute enables data to carry any type of content, whereby format and encoding might differ from that of the chosen event format. For example, an event rendered using the JSON envelope format might carry an XML payload in the data, and the consumer is informed by this attribute being set to "application/xml". The rules for how data content is rendered for different `datacontenttype` values are defined in the event format specifications.

- **`operation`**
  - Data type: String
  - Extension attribute
  - Represents the type of SQL operation that happened:
    -  INS for inserts
    -  UPD for updates
    -  DEL for deletes

- **`segmentindex`**
  - Data type: Integer
  - Extension attribute
  - Segment index, which denotes the position of the message within the logical message chunks. The segment index provides information about where the message stands in the sequence of logical message fragments. This field is always present. Use `logicalid`, `segmentindex`, and `finalsegment` fields to sort incoming events that represent a large SQL payload split according to the configured `max_message_size_kb` value.

- **`finalsegment`**
  - Data type: Boolean
  - Extension attribute
  - Indicates whether this segment is the final segment of the sequence. This field is always present and helps to identify whether a SQL event was split into subevents according to the configured `max_message_size_kb` value.

- **`data`**
  - Data type: Byte array
  - Optional CloudEvent attribute
  - Contains the domain-specific event data that describes the change. Deserialize the bytes as JSON or Avro binary according to the selected serialization format. The deserialized data follows the [CES data attribute Avro schema](#ces-data-attribute-avro-schema). For information about its fields, see [Data attribute format](#data-attribute-format).

> [!NOTE]
> Message splitting is separate from column-value truncation. Before CES serializes the `data` attribute, it truncates each streamed column value larger than 1 MB to 1 MB. CES then splits the formed event into message chunks as needed according to `max_message_size_kb`.

## Examples

### JSON message example - insert

```json
{
  "specversion": "1.0",
  "type": "com.microsoft.SQL.CES.DML.V1",
  "source": "\/",
  "id": "56cb8ff3-5c55-4f3b-a7f7-b044d1933ef6",
  "logicalid": "1bf2756a-c15f-4d2e-a2d5-7d3f9dbf85b0:000000B1000008A80007:00000000000000000001",
  "time": "2026-08-07T16:25:00.890Z",
  "datacontenttype": "application\/json",
  "operation": "INS",
  "segmentindex": 0,
  "finalsegment": true,
  "data": "{\"eventsource\":{\"db\":\"EmployeesDb\",\"schema\":\"dbo\",\"tbl\":\"Employees\",\"cols\":[{\"name\":\"Id\",\"type\":\"int\",\"index\":0},{\"name\":\"FirstName\",\"type\":\"nvarchar(50)\",\"index\":1},{\"name\":\"LastName\",\"type\":\"nvarchar(50)\",\"index\":2},{\"name\":\"SignupDate\",\"type\":\"datetime2(7)\",\"index\":3}],\"pkkey\":[{\"columnname\":\"Id\",\"value\":\"8\"}],\"transaction\":{\"commitlsn\":\"000000B1:000008A8:0007\",\"beginlsn\":\"000000B1:000008A8:0003\",\"sequencenumber\":1,\"finalevent\":false,\"committime\":\"2026-08-07T16:25:00.890Z\"}},\"eventrow\":{\"old\":\"{}\",\"current\":\"{\\\"Id\\\":\\\"8\\\",\\\"FirstName\\\":\\\"Nikola\\\",\\\"LastName\\\":\\\"Nikolic\\\",\\\"SignupDate\\\":\\\"2026-08-07 16:25:00.8833333\\\"}\"}}"
}
```

### JSON message example - update

```json
{
  "specversion": "1.0",
  "type": "com.microsoft.SQL.CES.DML.V1",
  "source": "\/",
  "id": "19221db1-a1b5-4ec7-8937-3fdf9d762abb",
  "logicalid": "1bf2756a-c15f-4d2e-a2d5-7d3f9dbf85b0:000000B1000009300009:00000000000000000001",
  "time": "2026-08-07T16:30:10.123Z",
  "datacontenttype": "application\/json",
  "operation": "UPD",
  "segmentindex": 0,
  "finalsegment": true,
  "data": "{\"eventsource\":{\"db\":\"EmployeesDb\",\"schema\":\"dbo\",\"tbl\":\"Employees\",\"cols\":[{\"name\":\"Id\",\"type\":\"int\",\"index\":0},{\"name\":\"FirstName\",\"type\":\"nvarchar(50)\",\"index\":1},{\"name\":\"LastName\",\"type\":\"nvarchar(50)\",\"index\":2},{\"name\":\"SignupDate\",\"type\":\"datetime2(7)\",\"index\":3}],\"pkkey\":[{\"columnname\":\"Id\",\"value\":\"8\"}],\"transaction\":{\"commitlsn\":\"000000B1:00000930:0009\",\"beginlsn\":\"000000B1:00000930:0002\",\"sequencenumber\":1,\"finalevent\":false,\"committime\":\"2026-08-07T16:30:10.123Z\"}},\"eventrow\":{\"old\":\"{\\\"Id\\\":\\\"8\\\",\\\"FirstName\\\":\\\"Nikola\\\",\\\"LastName\\\":\\\"Nikolic\\\",\\\"SignupDate\\\":\\\"2026-08-07 16:25:00.8833333\\\"}\",\"current\":\"{\\\"Id\\\":\\\"8\\\",\\\"FirstName\\\":\\\"Nikola\\\",\\\"LastName\\\":\\\"Nikolic-Smith\\\",\\\"SignupDate\\\":\\\"2026-08-07 16:25:00.8833333\\\"}\"}}"
}
```

### JSON message example - delete

```json
{
  "specversion": "1.0",
  "type": "com.microsoft.SQL.CES.DML.V1",
  "source": "\/",
  "id": "520f9a65-43d7-47f2-94f5-7ea14df635ed",
  "logicalid": "1bf2756a-c15f-4d2e-a2d5-7d3f9dbf85b0:000000B1000009700008:00000000000000000001",
  "time": "2026-08-07T16:35:42.450Z",
  "datacontenttype": "application\/json",
  "operation": "DEL",
  "segmentindex": 0,
  "finalsegment": true,
  "data": "{\"eventsource\":{\"db\":\"EmployeesDb\",\"schema\":\"dbo\",\"tbl\":\"Employees\",\"cols\":[{\"name\":\"Id\",\"type\":\"int\",\"index\":0},{\"name\":\"FirstName\",\"type\":\"nvarchar(50)\",\"index\":1},{\"name\":\"LastName\",\"type\":\"nvarchar(50)\",\"index\":2},{\"name\":\"SignupDate\",\"type\":\"datetime2(7)\",\"index\":3}],\"pkkey\":[{\"columnname\":\"Id\",\"value\":\"8\"}],\"transaction\":{\"commitlsn\":\"000000B1:00000970:0008\",\"beginlsn\":\"000000B1:00000970:0003\",\"sequencenumber\":1,\"finalevent\":false,\"committime\":\"2026-08-07T16:35:42.450Z\"}},\"eventrow\":{\"old\":\"{\\\"Id\\\":\\\"8\\\",\\\"FirstName\\\":\\\"Nikola\\\",\\\"LastName\\\":\\\"Nikolic-Smith\\\",\\\"SignupDate\\\":\\\"2026-08-07 16:25:00.8833333\\\"}\",\"current\":\"{}\"}}"
}
```

## Data attribute format

The `data` attribute is a byte array. Deserialize the bytes as JSON or Avro binary according to the selected serialization format. In both formats, the resulting `Data` record follows the [CES data attribute Avro schema](#ces-data-attribute-avro-schema) and contains two attributes:

- `eventsource`
- `eventrow`

```json
{
  "data": "{\"eventsource\": {}, \"eventrow\": {\"old\": \"{}\", \"current\": \"{}\"}}"
}
```

The following sections explain the deserialized attributes in greater detail.

### eventsource

Describes the metadata about the database and the table where the event occurred: 

- `db`
  - Data type: String
  - Description: The name of the database where the table is located.
  - Example: `EmployeesDb`

- `schema`
  - Data type: String
  - Description: The database schema that contains the table.
  - Example: `dbo`

- `tbl`
  - Data type: String
  - Description: The table in which the event occurred. 
  - Example: `Employees`

- `cols`
  - Data type: Array
  - Description: An array detailing the columns in the table.
    - `name` (**string**): The name of the column.
    - `type` (**string**): The SQL data type of the column, including its length, precision, or scale when applicable. Examples include `int`, `nvarchar(50)`, and `datetime2(7)`.
    - `index` (**integer**): The index or position of the column in the table.

- `pkkey`
  - Data type: Array
  - Description: Represents the primary key columns and their values for identifying the specific row.
    - `columnname` (**string**): The name of column used in the primary key.
    - `value` (**string**): The value for the column used in the primary key. This value helps uniquely identify the row.

- `transaction`
  - Data type: Object
  - Description: Describes the SQL transaction that contains the data operation.
    - `commitlsn` (**string**): The commit log sequence number (LSN) of the transaction.
    - `beginlsn` (**string**): The beginning LSN of the transaction.
    - `sequencenumber` (**integer**): The sequential number of the data operation within the transaction. Use this value to sort events within a transaction.
    - `finalevent` (**boolean**): Not in use. This field always has a value of `false`.
    - `committime` (**string**): The date and time when the transaction was committed in the database.

> [!NOTE]
> On SQL products configured with a non-UTC time zone, the `committime` field incorrectly includes a **Z** suffix, even though this field shows the local time of the publishing database. When the database uses UTC, the value and suffix agree. This problem is known, and a fix is pending in a future release of the feature.

### eventrow

Describes row-level changes and compares the old and current values of the fields in the record.

- **old** (object wrapped in string): Represents the values in the row before the event.
  - Each key-value pair consists of:
    - `<column_name>`: (string): The name of the column.
    - `<column_value>`:  (string/int/etc.): The previous value for that column.
- **current** (object wrapped in string): Represents the updated values in the row after the event.
  - Similar to the old object, with each key-value pair structured as:
    - `<column_name>` (string): The name of the column.
    - `<column_value>` (string/int/etc.): The new or current value for that column.

## CES CloudEvent Avro schema

```json
{
  "type": "record",
  "name": "ChangeEvent",
  "fields": [
    {
      "name": "specversion",
      "type": "string"
    },
    {
      "name": "type",
      "type": "string"
    },
    {
      "name": "source",
      "type": "string"
    },
    {
      "name": "id",
      "type": "string"
    },
    {
      "name": "logicalid",
      "type": "string"
    },
    {
      "name": "time",
      "type": "string"
    },
    {
      "name": "datacontenttype",
      "type": "string"
    },
    {
      "name": "operation",
      "type": "string"
    },
    {
      "name": "segmentindex",
      "type": "int"
    },
    {
      "name": "finalsegment",
      "type": "boolean"
    },
    {
      "name": "data",
      "type": "bytes"
    }
  ]
}
```

### CES data attribute Avro schema

Use the following schema when deserializing the `data` byte array in native JSON and Avro binary CloudEvents:

```json
{
  "name": "Data",
  "type": "record",
  "fields": [
    {
      "name": "eventsource",
      "type": {
        "name": "EventSource",
        "type": "record",
        "fields": [
          {
            "name": "db",
            "type": "string"
          },
          {
            "name": "schema",
            "type": "string"
          },
          {
            "name": "tbl",
            "type": "string"
          },
          {
            "name": "cols",
            "type": {
              "type": "array",
              "items": {
                "name": "Column",
                "type": "record",
                "fields": [
                  {
                    "name": "name",
                    "type": "string"
                  },
                  {
                    "name": "type",
                    "type": "string"
                  },
                  {
                    "name": "index",
                    "type": "int"
                  }
                ]
              }
            }
          },
          {
            "name": "pkkey",
            "type": {
              "type": "array",
              "items": {
                "name": "PkKey",
                "type": "record",
                "fields": [
                  {
                    "name": "columnname",
                    "type": "string"
                  },
                  {
                    "name": "value",
                    "type": "string"
                  }
                ]
              }
            }
          },
          {
            "name": "transaction",
            "type": {
              "name": "Transaction",
              "type": "record",
              "fields": [
                {
                  "name": "commitlsn",
                  "type": "string"
                },
                {
                  "name": "beginlsn",
                  "type": "string"
                },
                {
                  "name": "sequencenumber",
                  "type": "int"
                },
                {
                  "name": "finalevent",
                  "type": "boolean"
                },
                {
                  "name": "committime",
                  "type": "string"
                }
              ]
            }
          }
        ]
      }
    },
    {
      "name": "eventrow",
      "type": {
        "name": "EventRow",
        "type": "record",
        "fields": [
          {
            "name": "old",
            "type": "string"
          },
          {
            "name": "current",
            "type": "string"
          }
        ]
      }
    }
  ]
}
```


## Related content

- [What is change event streaming (preview)?](overview.md)
- [Configure change event streaming (preview) to Azure Event Hubs](configure.md)
- [Frequently asked CES questions](frequently-asked-questions-faq.yml)
