---
title: Work with Binary Data Using mssql-python
description: Learn how to insert, retrieve, and work with binary data types when you use the mssql-python driver for Microsoft SQL.
author: dlevy-msft-sql
ms.author: dlevy
ms.date: 07/16/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Work with binary data

The mssql-python driver supports `binary`, `varbinary`, and `image` data types for Microsoft SQL.

Microsoft SQL stores binary data in these column types:

| Type | Description | Max size |
| ------ | ------------- | ---------- |
| `binary(n)` | Fixed-length binary data. | 8,000 bytes |
| `varbinary(n)` | Variable-length binary data. | 8,000 bytes |
| `varbinary(max)` | Large binary data. | 2 GB |
| `image` | Legacy large binary data (deprecated). | 2 GB |

The mssql-python driver returns binary data as Python `bytes` objects.

**When to store binary in the database versus the file system:**

- Store in the database when files are small (under 1 MB), transactional consistency with other data is important, or you need to back up data and files together.
- Store in the file system (or Azure Blob Storage) when files are large (over 1 MB), you need CDN delivery, or you need to serve files directly to clients without database round-trips.
- For a middle ground, Microsoft SQL's [FILESTREAM](../../../relational-databases/blob/filestream-sql-server.md) feature stores data in the file system with transactional consistency.

## Insert binary data

Pass Python `bytes` objects as parameters; the driver maps them to **varbinary** columns.

### Insert bytes directly

Create a Python `bytes` object and pass it as a query parameter:

```python
import mssql_python

conn = mssql_python.connect(connection_string)
cursor = conn.cursor()

# Create a table to hold binary documents
cursor.execute("""
    CREATE TABLE #Documents (
        ID INT IDENTITY PRIMARY KEY,
        Name NVARCHAR(255),
        Content VARBINARY(MAX),
        Size INT NULL,
        ContentHash VARBINARY(32) NULL
    )
""")

# Binary data as bytes
binary_data = b'\x00\x01\x02\x03\x04\x05'

cursor.execute(
    "INSERT INTO #Documents (Name, Content) VALUES (%(name)s, %(content)s)",
    {"name": "sample.bin", "content": binary_data}
)
conn.commit()
```

### Insert from file

Read a file in binary mode and insert its contents:

```python
def insert_file(cursor, file_path: str, name: str):
    """Insert a file as binary data."""
    with open(file_path, "rb") as f:
        content = f.read()
    
    cursor.execute(
        "INSERT INTO #Documents (Name, Content, Size) VALUES (%(name)s, %(content)s, %(size)s)",
        {"name": name, "content": content, "size": len(content)}
    )

insert_file(cursor, "image.png", "profile_picture.png")
conn.commit()
```

### Insert image file

Store image files with metadata by detecting MIME types from file extensions:

```python
# Create a table to hold images
cursor.execute("""
    CREATE TABLE #Images (
        ID INT IDENTITY PRIMARY KEY,
        FileName NVARCHAR(255),
        FileSize INT NULL,
        ContentType NVARCHAR(100) NULL,
        Width INT NULL,
        Height INT NULL,
        ImageData VARBINARY(MAX),
        Description NVARCHAR(MAX) NULL
    )
""")

def insert_image(cursor, image_path: str, description: str):
    """Insert an image into the database."""
    import os
    
    with open(image_path, "rb") as f:
        image_data = f.read()
    
    cursor.execute("""
        INSERT INTO #Images (FileName, FileSize, ContentType, ImageData, Description)
        VALUES (%(filename)s, %(filesize)s, %(content_type)s, %(data)s, %(desc)s)
    """, {
        "filename": os.path.basename(image_path),
        "filesize": len(image_data),
        "content_type": get_content_type(image_path),
        "data": image_data,
        "desc": description
    })

def get_content_type(path: str) -> str:
    """Determine MIME type from file extension."""
    ext = path.lower().split(".")[-1]
    types = {
        "png": "image/png",
        "jpg": "image/jpeg",
        "jpeg": "image/jpeg",
        "gif": "image/gif",
        "pdf": "application/pdf",
    }
    return types.get(ext, "application/octet-stream")
```

## Retrieve binary data

The driver returns **varbinary** and **binary** column values as Python `bytes` objects.

### Fetch binary column

Query binary columns and inspect the returned `bytes` object:

```python
cursor.execute(
    "SELECT LargePhotoFileName, LargePhoto FROM Production.ProductPhoto WHERE ProductPhotoID = %(id)s",
    {"id": 70}
)
row = cursor.fetchone()

# LargePhoto is bytes
print(type(row.LargePhoto))  # <class 'bytes'>
print(len(row.LargePhoto))   # Number of bytes
```

### Save to file

Retrieve binary data and write it to disk:

```python
def save_photo(cursor, photo_id: int, output_path: str):
    """Retrieve binary data and save to file."""
    cursor.execute(
        "SELECT LargePhoto FROM Production.ProductPhoto WHERE ProductPhotoID = %(id)s",
        {"id": photo_id}
    )
    row = cursor.fetchone()
    
    if row and row.LargePhoto:
        with open(output_path, "wb") as f:
            f.write(row.LargePhoto)
        print(f"Saved {len(row.LargePhoto)} bytes to {output_path}")
    else:
        print("Photo not found or empty")

save_photo(cursor, 70, "output.gif")
```

### Export binary rows to files

Export multiple binary rows to local files:

```python
import os

def export_photos(cursor, output_dir: str):
    """Export product photos to a directory."""
    os.makedirs(output_dir, exist_ok=True)
    
    cursor.execute("""
        SELECT ProductPhotoID, LargePhotoFileName, LargePhoto
        FROM Production.ProductPhoto
        WHERE ProductPhotoID > 1
    """)
    
    count = 0
    for row in cursor:
        output_path = os.path.join(output_dir, f"{row.ProductPhotoID}_{row.LargePhotoFileName}")
        with open(output_path, "wb") as f:
            f.write(row.LargePhoto)
        count += 1
    
    print(f"Exported {count} photos to {output_dir}")

export_photos(cursor, "./photos")
```

### Insert NULL binary values

Pass `None` to insert a SQL NULL into a binary column. Because `#Documents` is a temporary table, declare the parameter types by using `setinputsizes()` first so the driver binds `Content` as `varbinary`:

```python
cursor.setinputsizes([(mssql_python.SQL_WVARCHAR, 255, 0), (mssql_python.SQL_VARBINARY, 0, 0)])
cursor.execute(
    "INSERT INTO #Documents (Name, Content) VALUES (%(name)s, %(content)s)",
    {"name": "empty", "content": None}
)
```

> [!NOTE]
> The driver normally infers parameter types through `SQLDescribeParam`, but it can't resolve type metadata for a **temporary table** or **table variable**. Inserting `None` into a `binary` or `varbinary` column of a temp object without `setinputsizes()` raises `ProgrammingError: Implicit conversion from data type varchar to varbinary(max) is not allowed`. Pass one entry per parameter, in order, and use a SQL type constant such as `mssql_python.SQL_VARBINARY` for the binary column. For a regular (permanent) table, the driver resolves the type automatically and you can pass `None` directly.

## Large binary data

For files over a few megabytes, insert the full content in a single **varbinary(max)** write instead of reading the file in small chunks.

### Stream large files

For large files, process in chunks:

```python
def insert_large_file(conn, table: str, file_path: str, chunk_size: int = 8192):
    """Insert large file in chunks using updatetext-style approach."""
    # Validate table name to prevent SQL injection
    import re
    if not re.match(r'^#{0,2}[A-Za-z_][A-Za-z0-9_.]*$', table):
        raise ValueError(f"Invalid table name: {table}")

    cursor = conn.cursor()
    
    # Get file size
    import os
    file_size = os.path.getsize(file_path)
    
    # Insert initial row with empty binary
    cursor.execute(f"""
        INSERT INTO {table} (Name, Content, Size)
        VALUES (%(name)s, 0x, %(size)s);
        SELECT SCOPE_IDENTITY();
    """, {"name": os.path.basename(file_path), "size": file_size})
    
    row_id = cursor.fetchval()
    
    # For modern Microsoft SQL, better to use varbinary(max) and single insert
    # This example shows streaming approach
    with open(file_path, "rb") as f:
        content = f.read()
    
    cursor.execute(f"""
        UPDATE {table} SET Content = %(content)s WHERE ID = %(id)s
    """, {"content": content, "id": row_id})
    
    conn.commit()
    return row_id
```

### Use bulk copy for binary data

Use the `bulkcopy()` method to efficiently insert multiple binary files in a single operation.

> [!IMPORTANT]
> `bulkcopy()` loads data over a **separate connection** to the server, so the destination table must already exist and be **committed and visible to other sessions**. If you create the table in the same script with autocommit off, call `conn.commit()` before `bulkcopy()`. Local temporary tables (`#name`) aren't supported because they're private to the session that created them. An uncommitted or unreachable destination causes `bulkcopy()` to fail with a `Failed to retrieve destination metadata` timeout.

By default, `bulkcopy()` maps each value in a row to a table column by ordinal position, so every column must have a value in order. When the table has an identity column or you only populate some columns, pass `column_mappings` to name the target columns explicitly. Otherwise the values shift onto the wrong columns and `bulkcopy()` fails.

```python
def bulk_insert_files(conn, table: str, file_paths: list[str]):
    """Bulk insert multiple binary files."""
    import os

    data = []
    for path in file_paths:
        with open(path, "rb") as f:
            content = f.read()
        data.append((os.path.basename(path), content, len(content)))

    cursor = conn.cursor()
    result = cursor.bulkcopy(table, data, column_mappings=["Name", "Content", "Size"])
    conn.commit()
    return result["rows_copied"]
```

## Binary data operations

Use Microsoft SQL's `HASHBYTES` function to compare binary content without fetching full values.

### Compare binary data

Find binary rows that share identical content by computing and comparing content hashes in Microsoft SQL Server:

```python
# Find rows with identical binary content by hash
cursor.execute("""
    SELECT LargePhotoFileName, HASHBYTES('SHA2_256', LargePhoto) AS PhotoHash
    FROM Production.ProductPhoto
    WHERE ProductPhotoID > 1
""")

hashes = {}
for row in cursor:
    hash_value = row.PhotoHash  # bytes
    if hash_value in hashes:
        print(f"Duplicate: {row.LargePhotoFileName} matches {hashes[hash_value]}")
    else:
        hashes[hash_value] = row.LargePhotoFileName
```

### Compute hash in Python

Compute SHA-256 hashes in Python and store them alongside binary data:

```python
import hashlib

def insert_with_hash(cursor, name: str, content: bytes):
    """Insert binary data with computed hash."""
    content_hash = hashlib.sha256(content).digest()
    
    cursor.execute("""
        INSERT INTO #Documents (Name, Content, ContentHash)
        VALUES (%(name)s, %(content)s, %(hash)s)
    """, {"name": name, "content": content, "hash": content_hash})
```

### Encode and decode binary data
Convert binary data to and from base64-encoded strings:
```python
import base64

# Store base64-encoded string
def insert_base64(cursor, name: str, base64_data: str):
    """Insert base64-encoded data as binary."""
    binary_data = base64.b64decode(base64_data)
    cursor.execute(
        "INSERT INTO #Documents (Name, Content) VALUES (%(name)s, %(content)s)",
        {"name": name, "content": binary_data}
    )

# Retrieve as base64
def get_as_base64(cursor, doc_id: int) -> str:
    """Retrieve binary data as base64 string."""
    cursor.execute("SELECT Content FROM #Documents WHERE ID = %(id)s", {"id": doc_id})
    row = cursor.fetchone()
    return base64.b64encode(row.Content).decode("utf-8")
```

## Work with specific binary formats

These examples show how to validate and insert common file formats before storing them.

### PDF documents

Verify PDF file signatures before inserting them:

```python
def insert_pdf(cursor, pdf_path: str, title: str):
    """Insert a PDF document."""
    with open(pdf_path, "rb") as f:
        pdf_data = f.read()
    
    # Verify it's a PDF (magic bytes)
    if not pdf_data.startswith(b'%PDF'):
        raise ValueError("Not a valid PDF file")
    
    cursor.execute(
        "INSERT INTO #Documents (Name, Content) VALUES (%(title)s, %(data)s)",
        {"title": title, "data": pdf_data}
    )
```

### Images with PIL/Pillow

Resize images using Pillow (PIL) before storing them to save space:

```python
from PIL import Image
import io
import os

def insert_resized_image(cursor, image_path: str, max_size: tuple = (800, 600)):
    """Insert a resized image."""
    # Open and resize
    img = Image.open(image_path)
    img.thumbnail(max_size, Image.LANCZOS)
    
    # Convert to bytes
    buffer = io.BytesIO()
    img.save(buffer, format=img.format or "PNG")
    image_bytes = buffer.getvalue()
    
    cursor.execute("""
        INSERT INTO #Images (FileName, Width, Height, ImageData)
        VALUES (%(name)s, %(width)s, %(height)s, %(data)s)
    """, {
        "name": os.path.basename(image_path),
        "width": img.width,
        "height": img.height,
        "data": image_bytes
    })

def get_image_as_pil(cursor, image_id: int) -> Image.Image:
    """Retrieve image as PIL Image object."""
    cursor.execute("SELECT ImageData FROM #Images WHERE ID = %(id)s", {"id": image_id})
    row = cursor.fetchone()
    return Image.open(io.BytesIO(row.ImageData))
```

### Compressed data

Reduce storage by compressing binary data with gzip before insertion:

```python
import gzip

def insert_compressed(cursor, name: str, data: bytes):
    """Insert data with gzip compression."""
    compressed = gzip.compress(data)
    
    cursor.execute(
        "INSERT INTO #Documents (Name, Content, Size) VALUES (%(name)s, %(content)s, %(size)s)",
        {"name": name, "content": compressed, "size": len(data)}
    )

def get_decompressed(cursor, data_id: int) -> bytes:
    """Retrieve and decompress data."""
    cursor.execute(
        "SELECT Content FROM #Documents WHERE ID = %(id)s",
        {"id": data_id}
    )
    row = cursor.fetchone()
    return gzip.decompress(row.Content)
```

## Best practices

Apply these guidelines to choose the right column type and storage strategy for binary data.

### Use appropriate column types

Choose the column type based on your data's characteristics:

For different binary use cases, select the appropriate Microsoft SQL data type:

```sql
-- For small fixed-size binary (for example, hashes, UUIDs)
binary(32)      -- SHA-256 hash

-- For variable-size binary up to 8KB (for example, thumbnails, small icons)
varbinary(8000)

-- For large binary data (for example, documents, images)
varbinary(max)  -- Up to 2GB
```

### Consider FILESTREAM for large files

For large files (over 1 MB), consider the Microsoft SQL [FILESTREAM](../../../relational-databases/blob/filestream-sql-server.md) feature, which stores data in the file system. FILESTREAM requires [server-side configuration](../../../relational-databases/blob/enable-and-configure-filestream.md) before use:

```python
# FILESTREAM-enabled databases store large binaries more efficiently
# Access is still through normal queries but storage is file-based
large_binary_data = b"\x25\x50\x44\x46" + b"\x00" * 100  # sample data

cursor.execute("""
    CREATE TABLE ##FileStreamDemo (Name NVARCHAR(100), Document VARBINARY(MAX))
""")
cursor.execute("""
    INSERT INTO ##FileStreamDemo (Name, Document)
    VALUES (%(name)s, %(content)s)
""", {"name": "large_doc.pdf", "content": large_binary_data})

cursor.execute("SELECT Name, DATALENGTH(Document) AS DocSize FROM ##FileStreamDemo")
row = cursor.fetchone()
print(f"{row.Name}: {row.DocSize} bytes")
```

### Validate binary data

Validate file types by checking magic bytes (file signatures) before storing binary data:

```python
def insert_safe_image(cursor, name: str, data: bytes):
    """Insert image with validation."""
    # Check file signatures (magic bytes)
    signatures = {
        b'\x89PNG': 'image/png',
        b'\xff\xd8\xff': 'image/jpeg',
        b'GIF87a': 'image/gif',
        b'GIF89a': 'image/gif',
    }
    
    content_type = None
    for sig, mime in signatures.items():
        if data.startswith(sig):
            content_type = mime
            break
    
    if content_type is None:
        raise ValueError("Unknown or unsupported image format")
    
    cursor.execute("""
        INSERT INTO #Images (FileName, ContentType, ImageData)
        VALUES (%(name)s, %(type)s, %(data)s)
    """, {"name": name, "type": content_type, "data": data})
```

## Related content

- [Data type mappings for mssql-python](data-type-mappings.md)
- [Use bulk copy with mssql-python](bulk-copy.md)
- [Handle NULL values](null-handling.md)
