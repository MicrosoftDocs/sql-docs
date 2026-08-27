---
title: Use Spatial Data with mssql-python
description: Learn how to work with Microsoft SQL geography and geometry data types using the mssql-python driver for location-based applications.
author: dlevy-msft-sql
ms.author: dlevy
ms.date: 08/21/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Use spatial data with mssql-python

Microsoft SQL provides two spatial data types that you can use through the mssql-python driver. Choose the type based on what your coordinates represent:

| Type | Description | Use case |
| ------ | ------------- | ---------- |
| `geography` | Round-earth coordinate system | GPS coordinates, maps, anything on the Earth's surface. Distances are in meters. SRID 4326 ([WGS 84](https://epsg.io/4326)) is standard for GPS. |
| `geometry` | Flat-plane coordinate system | Floor plans, CAD drawings, game worlds, or any Cartesian coordinate system. Distances are in the units of your coordinate system. |

## Insert spatial data

Use Microsoft SQL's constructor functions such as `geography::Point()` or pass Well-Known Text (WKT) strings.

### Geography data (points)

Insert geographic points using the Point constructor with latitude, longitude, and SRID.

```python
import mssql_python

connection_string = "Server=<server>.database.windows.net;Database=AdventureWorks2025;Authentication=ActiveDirectoryDefault;Encrypt=yes"

conn = mssql_python.connect(connection_string)
cursor = conn.cursor()

# Insert a geographic point (longitude, latitude)
# Note: Microsoft SQL uses (longitude, latitude) order
cursor.execute("CREATE TABLE #Locations (Name NVARCHAR(100), GeoLocation GEOGRAPHY)")
cursor.execute("""
    INSERT INTO #Locations (Name, GeoLocation)
    VALUES (%(name)s, geography::Point(%(lat)s, %(lon)s, 4326))
""", {"name": "Seattle", "lat": 47.6062, "lon": -122.3321})
conn.commit()
```

### Geography from WKT ([Well-Known Text](https://www.ogc.org/standard/wkt-crs/))

Insert geographic data using Well-Known Text format, which supports points, linestrings, and polygons.

```python
# Well-Known Text format
wkt_point = "POINT(-122.3321 47.6062)"
wkt_line = "LINESTRING(-122.3321 47.6062, -122.4194 37.7749)"
wkt_polygon = "POLYGON((-122.40 47.60, -122.30 47.60, -122.30 47.65, -122.40 47.65, -122.40 47.60))"

cursor.execute("DROP TABLE IF EXISTS #Locations")
cursor.execute("CREATE TABLE #Locations (Name NVARCHAR(100), GeoLocation GEOGRAPHY)")
cursor.execute("""
    INSERT INTO #Locations (Name, GeoLocation)
    VALUES (%(name)s, geography::STGeomFromText(%(wkt)s, 4326))
""", {"name": "Route", "wkt": wkt_line})
conn.commit()
```

### Geometry data

Insert flat-plane geometry data using points and polygons for CAD drawings or floor plans.

```python
# Insert a geometry point (flat coordinate system)
cursor.execute("CREATE TABLE #FloorPlan (RoomName NVARCHAR(100), RoomShape GEOMETRY)")
cursor.execute("""
    INSERT INTO #FloorPlan (RoomName, RoomShape)
    VALUES (%(name)s, geometry::Point(%(x)s, %(y)s, 0))
""", {"name": "Office 101", "x": 50.0, "y": 100.0})

# Insert a geometry polygon
room_wkt = "POLYGON((0 0, 0 10, 20 10, 20 0, 0 0))"
cursor.execute("""
    INSERT INTO #FloorPlan (RoomName, RoomShape)
    VALUES (%(name)s, geometry::STGeomFromText(%(wkt)s, 0))
""", {"name": "Conference Room", "wkt": room_wkt})
conn.commit()
```

## Query spatial data

Use spatial methods like `STAsText()` and the `Lat`/`Long` properties to retrieve coordinates in a readable format.

### Retrieve as text

Retrieve spatial coordinates as Well-Known Text format along with latitude and longitude properties.

```python
cursor.execute("""
    SELECT 
        City,
        SpatialLocation.STAsText() AS LocationWKT,
        SpatialLocation.Lat AS Latitude,
        SpatialLocation.Long AS Longitude
    FROM Person.Address
    WHERE SpatialLocation IS NOT NULL
""")

for row in cursor.fetchall()[:5]:
    print(f"{row.City}: {row.Latitude}, {row.Longitude}")
    print(f"  WKT: {row.LocationWKT}")
```

### Retrieve as GeoJSON

Microsoft SQL supports [GeoJSON](https://datatracker.ietf.org/doc/html/rfc7946) conversion. In SQL Server 2017+, you can use string functions to build GeoJSON directly, or convert in Python as shown below:

```python
cursor.execute("""
    SELECT 
        City,
        SpatialLocation.STAsText() AS WKT
    FROM Person.Address
    WHERE SpatialLocation IS NOT NULL AND City = %(city)s
""", {"city": "Seattle"})

for row in cursor:
    print(f"{row.City}: {row.WKT}")
```

### Find nearby points

Find all locations within a specified distance of a reference point using spatial distance functions.

```python
# Find locations within 10 km of Seattle
cursor.execute("""
    DECLARE @seattle geography = geography::Point(47.6062, -122.3321, 4326);
    
    SELECT 
        City,
        SpatialLocation.STDistance(@seattle) / 1000 AS DistanceKM
    FROM Person.Address
    WHERE SpatialLocation IS NOT NULL
      AND SpatialLocation.STDistance(@seattle) < 50000  -- 50 km in meters
    ORDER BY SpatialLocation.STDistance(@seattle)
""")

for row in cursor.fetchall()[:5]:
    print(f"{row.City}: {row.DistanceKM:.2f} km away")
```

### Find points within polygon

Find all points that intersect with or fall within a geographic polygon region.

```python
# Find all addresses within a region
cursor.execute("""
    DECLARE @region geography = geography::STPolyFromText(
        'POLYGON((-122.5 47.5, -122.2 47.5, -122.2 47.7, -122.5 47.7, -122.5 47.5))',
        4326
    );
    
    SELECT City, SpatialLocation.STAsText() AS Location
    FROM Person.Address
    WHERE SpatialLocation IS NOT NULL
      AND @region.STIntersects(SpatialLocation) = 1
""")
```

## Calculate distances

Microsoft SQL's `STDistance()` method returns distances in meters for geography types.

### Distance between two points

Create a helper function to calculate the distance in kilometers between two geographic points.

```python
def get_distance_km(cursor, point1: tuple, point2: tuple) -> float:
    """Calculate distance between two points in kilometers."""
    cursor.execute("""
        DECLARE @point1 geography = geography::Point(%(lat1)s, %(lon1)s, 4326);
        DECLARE @point2 geography = geography::Point(%(lat2)s, %(lon2)s, 4326);
        SELECT @point1.STDistance(@point2) / 1000 AS DistanceKM;
    """, {
        "lat1": point1[0], "lon1": point1[1],
        "lat2": point2[0], "lon2": point2[1]
    })
    return cursor.fetchval()

# Seattle to San Francisco
distance = get_distance_km(cursor, (47.6062, -122.3321), (37.7749, -122.4194))
print(f"Distance: {distance:.2f} km")
```

### Calculate area
Calculate the area of a geographic region in square kilometers.
```python
cursor.execute("""
    DECLARE @region geography = geography::STPolyFromText(
        'POLYGON((-122.5 47.5, -122.2 47.5, -122.2 47.7, -122.5 47.7, -122.5 47.5))',
        4326
    );
    SELECT 
        'Seattle Region' AS Name,
        @region.STArea() / 1000000 AS AreaSqKm
""")

row = cursor.fetchone()
print(f"{row.Name}: {row.AreaSqKm:.2f} sq km")
```

## Spatial operations

Microsoft SQL supports set operations on spatial objects, including union, intersection, and buffer.

### Union of shapes

Combine two geographic polygons into a single shape and calculate the total area.

```python
cursor.execute("""
    DECLARE @parcel1 geography = geography::STPolyFromText(
        'POLYGON((-122.35 47.60, -122.33 47.60, -122.33 47.62, -122.35 47.62, -122.35 47.60))',
        4326
    );
    DECLARE @parcel2 geography = geography::STPolyFromText(
        'POLYGON((-122.34 47.61, -122.32 47.61, -122.32 47.63, -122.34 47.63, -122.34 47.61))',
        4326
    );
    DECLARE @combined geography = @parcel1.STUnion(@parcel2);
    
    SELECT @combined.STAsText() AS CombinedWKT,
           @combined.STArea() / 1000000 AS TotalAreaKM
""")

row = cursor.fetchone()
print(f"Combined area: {row.TotalAreaKM:.2f} sq km")
```

### Intersection
Find the overlapping area where two geographic regions intersect.
```python
polygon1_wkt = "POLYGON((-122.35 47.60, -122.33 47.60, -122.33 47.62, -122.35 47.62, -122.35 47.60))"
polygon2_wkt = "POLYGON((-122.34 47.61, -122.32 47.61, -122.32 47.63, -122.34 47.63, -122.34 47.61))"

cursor.execute("""
    DECLARE @region1 geography = geography::STPolyFromText(%(wkt1)s, 4326);
    DECLARE @region2 geography = geography::STPolyFromText(%(wkt2)s, 4326);
    
    SELECT 
        @region1.STIntersection(@region2).STAsText() AS IntersectionWKT,
        @region1.STIntersection(@region2).STArea() / 1000000 AS AreaKM
""", {"wkt1": polygon1_wkt, "wkt2": polygon2_wkt})
```

### Buffer (expand area)

Create a buffer zone around a geographic point and find all locations within the buffer radius.

```python
# Find all addresses within 5km buffer of a point
cursor.execute("""
    DECLARE @center geography = geography::Point(47.6062, -122.3321, 4326);
    DECLARE @buffer geography = @center.STBuffer(5000);  -- 5 km buffer
    
    SELECT City
    FROM Person.Address
    WHERE SpatialLocation IS NOT NULL
      AND @buffer.STIntersects(SpatialLocation) = 1
""")
```

## Python integration

Retrieve WKT strings from Microsoft SQL and parse them with the Shapely library for client-side geometry processing.

### With Shapely library

Install by running `pip install shapely`.

```python
from shapely import wkt
from shapely.geometry import Point, Polygon

# Retrieve spatial data from Person.Address
cursor.execute("""
    SELECT City, SpatialLocation.STAsText() AS WKT
    FROM Person.Address
    WHERE SpatialLocation IS NOT NULL AND City IN ('Seattle', 'Redmond')
""")

for row in cursor:
    # Parse WKT into Shapely geometry
    geom = wkt.loads(row.WKT)
    
    if isinstance(geom, Point):
        print(f"{row.City}: Point at ({geom.x}, {geom.y})")
    elif isinstance(geom, Polygon):
        print(f"{row.City}: Polygon with area {geom.area}")
```

### Create spatial data with Shapely

Create geometric objects using the Shapely library and convert them to WKT format for insertion into Microsoft SQL.

```python
from shapely.geometry import Point, Polygon, LineString
from shapely import wkt

# Create geometries in Python
seattle = Point(-122.3321, 47.6062)
seattle_wkt = wkt.dumps(seattle)

route = LineString([(-122.3321, 47.6062), (-122.4194, 37.7749)])
route_wkt = wkt.dumps(route)

# Insert into a temp table
cursor.execute("CREATE TABLE #Routes (Name NVARCHAR(100), Path NVARCHAR(MAX))")
cursor.execute("""
    INSERT INTO #Routes (Name, Path)
    VALUES (%(name)s, %(wkt)s)
""", {"name": "Seattle to SF", "wkt": route_wkt})

cursor.execute("SELECT Name, Path FROM #Routes")
row = cursor.fetchone()
print(f"{row.Name}: {row.Path[:40]}...")
```

### Convert to GeoJSON
Convert spatial data from WKT format to GeoJSON for web-based applications and mapping services.
```python
import json
from shapely import wkt
from shapely.geometry import mapping

cursor.execute("""
    SELECT City, SpatialLocation.STAsText() AS WKT
    FROM Person.Address
    WHERE SpatialLocation IS NOT NULL AND City = 'Seattle'
""")

row = cursor.fetchone()
geom = wkt.loads(row.WKT)

geojson = {
    "type": "Feature",
    "properties": {"name": row.City},
    "geometry": mapping(geom)
}

print(json.dumps(geojson, indent=2))
```

### GeoDataFrame integration
Load spatial data from Microsoft SQL into a GeoPandas GeoDataFrame for advanced geospatial analysis.
```python
import geopandas as gpd
from shapely import wkt
import pandas as pd

cursor.execute("""
    SELECT AddressID, City, SpatialLocation.STAsText() AS WKT
    FROM Person.Address
    WHERE SpatialLocation IS NOT NULL AND City = 'Seattle'
""")

# Build DataFrame
rows = cursor.fetchall()
df = pd.DataFrame(
    [(r.AddressID, r.City, r.WKT) for r in rows],
    columns=['id', 'city', 'wkt']
)

# Convert to GeoDataFrame
df['geometry'] = df['wkt'].apply(wkt.loads)
gdf = gpd.GeoDataFrame(df, geometry='geometry', crs="EPSG:4326")

# Now use GeoPandas operations
print(gdf.head())
```

## Spatial indexes

Create spatial indexes in Microsoft SQL to accelerate queries on large spatial datasets.

```sql
-- Geography index on Person.Address
CREATE SPATIAL INDEX SIX_Address_SpatialLocation
ON Person.Address(SpatialLocation)
USING GEOGRAPHY_GRID
WITH (
    GRIDS = (LEVEL_1 = MEDIUM, LEVEL_2 = MEDIUM, LEVEL_3 = MEDIUM, LEVEL_4 = MEDIUM),
    CELLS_PER_OBJECT = 16
);

-- Geometry index (example with custom table)
CREATE SPATIAL INDEX SIX_FloorPlan_RoomShape
ON dbo.FloorPlan(RoomShape)
USING GEOMETRY_GRID
WITH (
    BOUNDING_BOX = (0, 0, 1000, 1000),
    GRIDS = (LEVEL_1 = HIGH, LEVEL_2 = HIGH, LEVEL_3 = HIGH, LEVEL_4 = HIGH)
);
```

## Performance tips

Apply these techniques to keep spatial queries efficient at scale.

### Use spatial index hints

Use spatial indexes to accelerate queries on large spatial datasets.

```python
cursor.execute("""
    DECLARE @region geography = geography::STPolyFromText(
        'POLYGON((-122.5 47.5, -122.2 47.5, -122.2 47.7, -122.5 47.7, -122.5 47.5))',
        4326
    );

    SELECT City
    FROM Person.Address
    WHERE SpatialLocation IS NOT NULL
      AND SpatialLocation.STIntersects(@region) = 1
""")
```

### Filter first, then calculate

Optimize spatial queries by first applying a fast bounding box filter, then performing precise distance calculations.

```python
# Approximate filter with bounding box, then precise calculation
cursor.execute("""
    DECLARE @center geography = geography::Point(47.6062, -122.3321, 4326);
    
    SELECT City, SpatialLocation.STDistance(@center) AS Distance
    FROM Person.Address
    WHERE SpatialLocation IS NOT NULL
      AND SpatialLocation.Filter(@center.STBuffer(10000)) = 1  -- Fast bounding box filter
      AND SpatialLocation.STDistance(@center) < 10000         -- Precise distance check
    ORDER BY Distance
""")
```

### Reduce precision for display

Simplify spatial geometries for display by reducing coordinate precision.

```python
cursor.execute("""
    SELECT 
        City,
        SpatialLocation.Reduce(100).STAsText() AS SimplifiedWKT  -- 100 meter tolerance
    FROM Person.Address
    WHERE SpatialLocation IS NOT NULL AND City = 'Seattle'
""")
```

## Coordinate reference systems

The SRID determines the coordinate system and affects how Microsoft SQL calculates distances and areas.

### Common SRID values

The SRID (Spatial Reference Identifier) defines the coordinate system for your data. Using the wrong SRID produces incorrect distance and area calculations.

| SRID | Name | Use case |
| ------ | ------ | ---------- |
| 4326 | [WGS 84](https://epsg.io/4326) | GPS coordinates, web mapping |
| 4269 | [NAD 83](https://epsg.io/4269) | North American surveys |
| 0 | No SRID | Flat geometry, local coordinates |

### Convert between SRIDs

Retrieve spatial data and check its SRID to verify the coordinate system.

```python
cursor.execute("""
    -- Geography is always round-earth, but SRID defines datum
    SELECT SpatialLocation.STAsText() AS WKT,
           SpatialLocation.STSrid AS SRID
    FROM Person.Address
    WHERE SpatialLocation IS NOT NULL
""")
```

## Best practices

Apply these guidelines to work with spatial data correctly and efficiently.

### Validate geometry

Check spatial data for validity and identify any geometric problems before processing.

```python
cursor.execute("""
    SELECT 
        City,
        SpatialLocation.STIsValid() AS IsValid,
        SpatialLocation.IsValidDetailed() AS InvalidReason
    FROM Person.Address
    WHERE SpatialLocation IS NOT NULL
      AND SpatialLocation.STIsValid() = 0
""")

for row in cursor:
    print(f"Invalid: {row.City} - {row.InvalidReason}")
```

### Make geometry valid

Use the `MakeValid()` method to automatically correct invalid geometric shapes.

```python
# Example with a temp table
cursor.execute("""
    CREATE TABLE #SpatialFix (Name NVARCHAR(100), GeoLocation GEOGRAPHY);
    INSERT INTO #SpatialFix VALUES ('Test', geography::STGeomFromText('POLYGON((0 0, 0 1, 1 0, 0 0))', 4326));
    UPDATE #SpatialFix
    SET GeoLocation = GeoLocation.MakeValid()
    WHERE GeoLocation.STIsValid() = 0
""")
```

### Choose the right type

The choice between `geography` and `geometry` determines how Microsoft SQL calculates distances and areas:

- **geography**: Real-world locations (GPS points), earth surface calculations, distances in meters.
- **geometry**: Flat surfaces (floor plans, CAD), Cartesian coordinate systems, or when SRID doesn't matter.

## Related content

- [Data type mappings for mssql-python](data-type-mappings.md)
- [Spatial Data](../../../relational-databases/spatial/spatial-data-sql-server.md)
