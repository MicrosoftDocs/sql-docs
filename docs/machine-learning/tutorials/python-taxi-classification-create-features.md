---
title: "Python tutorial: Create data features"
titleSuffix: SQL machine learning
description: In part three of this five-part tutorial series, you'll add calculations to stored procedures for use in Python machine learning models with SQL machine learning.
ms.service: sql
ms.subservice: machine-learning

ms.date: 09/17/2021
ms.topic: tutorial
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15||>=azuresqldb-mi-current"
---

# Python tutorial: Create Data Features using T-SQL
[!INCLUDE [SQL Server 2017 SQL MI](../../includes/applies-to-version/sqlserver2017-asdbmi.md)]

In part three of this five-part tutorial series, you'll learn how to create features from raw data by using a [!INCLUDE[tsql](../../includes/tsql-md.md)] function. You'll then call that function from a SQL stored procedure to create a table that contains the feature values.

The process of *feature engineering*, creating features from the raw data, can be a critical step in advanced analytics modeling.

In this article, you'll:

> [!div class="checklist"]
> + Modify a custom function to calculate trip distance
> + Save the features using another custom function

In [part one](python-taxi-classification-introduction.md), you installed the prerequisites and restored the sample database.

In [part two](python-taxi-classification-explore-data.md), you explored the sample data and generated some plots.

In [part four](python-taxi-classification-train-model.md), you'll load the modules and call the necessary functions to create and train the model using a SQL Server stored procedure.

In [part five](python-taxi-classification-deploy-model.md), you'll learn how to operationalize the models that you trained and saved in part four.

## Define the Function

The distance values reported in the original data are based on the reported meter distance, and don't necessarily represent geographical distance or distance traveled. Therefore, you'll need to calculate the direct distance between the pick-up and drop-off points, by using the coordinates available in the source NYC Taxi dataset. You can do this by using the [Haversine formula](https://en.wikipedia.org/wiki/Haversine_formula) in a custom [!INCLUDE[tsql](../../includes/tsql-md.md)] function.

You'll use one custom T-SQL function, _fnCalculateDistance_, to compute the distance using the Haversine formula, and use a second custom T-SQL function, _fnEngineerFeatures_, to create a table containing all the features.

### Calculate trip distance using _fnCalculateDistance_

The function _fnCalculateDistance_ is included in the sample database. Take a minute to review the code:

1. In [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], expand **Programmability**, expand **Functions** and then **Scalar-valued functions**.
1. Right-click _fnCalculateDistance_, and select **Modify** to open the [!INCLUDE[tsql](../../includes/tsql-md.md)] script in a new query window.

   It should look something like this:
  
   ```sql
   CREATE FUNCTION [dbo].[fnCalculateDistance] (@Lat1 float, @Long1 float, @Lat2 float, @Long2 float)
   -- User-defined function that calculates the direct distance between two geographical coordinates
   RETURNS float
   AS
   BEGIN
     DECLARE @distance decimal(28, 10)
     -- Convert to radians
     SET @Lat1 = @Lat1 / 57.2958
     SET @Long1 = @Long1 / 57.2958
     SET @Lat2 = @Lat2 / 57.2958
     SET @Long2 = @Long2 / 57.2958
     -- Calculate distance
     SET @distance = (SIN(@Lat1) * SIN(@Lat2)) + (COS(@Lat1) * COS(@Lat2) * COS(@Long2 - @Long1))
     --Convert to miles
     IF @distance <> 0
     BEGIN
       SET @distance = 3958.75 * ATAN(SQRT(1 - POWER(@distance, 2)) / @distance);
     END
     RETURN @distance
   END
   GO
   ```

**Notes:**

+ The function is a scalar-valued function, returning a single data value of a predefined type.
+ The function takes latitude and longitude values as inputs, obtained from trip pick-up and drop-off locations. The Haversine formula converts locations to radians and uses those values to compute the direct distance in miles between those two locations.

### Save the features using _fnEngineerFeatures_

To add the computed value to a table that can be used for training the model, you'll use the custom T-SQL function, _fnEngineerFeatures_. This function is a table-valued function that takes multiple columns as inputs, and outputs a table with multiple feature columns.  The purpose of this function is to create a feature set for use in building a model. The function _fnEngineerFeatures_ calls the previously created T-SQL function, _fnCalculateDistance_, to get the direct distance between pickup and dropoff locations.

Take a minute to review the code:

```sql
CREATE FUNCTION [dbo].[fnEngineerFeatures] (
@passenger_count int = 0,
@trip_distance float = 0,
@trip_time_in_secs int = 0,
@pickup_latitude float = 0,
@pickup_longitude float = 0,
@dropoff_latitude float = 0,
@dropoff_longitude float = 0)
RETURNS TABLE
AS
  RETURN
  (
  -- Add the SELECT statement with parameter references here
  SELECT
    @passenger_count AS passenger_count,
    @trip_distance AS trip_distance,
    @trip_time_in_secs AS trip_time_in_secs,
    [dbo].[fnCalculateDistance](@pickup_latitude, @pickup_longitude, @dropoff_latitude, @dropoff_longitude) AS direct_distance
  )
GO
```
  
To verify that this function works, you can use it to calculate the geographical distance for those trips where the metered distance was 0 but the pick-up and drop-off locations were different.
  
  ```sql
      SELECT tipped, fare_amount, passenger_count,(trip_time_in_secs/60) as TripMinutes,
      trip_distance, pickup_datetime, dropoff_datetime,
      dbo.fnCalculateDistance(pickup_latitude, pickup_longitude,  dropoff_latitude, dropoff_longitude) AS direct_distance
      FROM nyctaxi_sample
      WHERE pickup_longitude != dropoff_longitude and pickup_latitude != dropoff_latitude and trip_distance = 0
      ORDER BY trip_time_in_secs DESC
  ```
  
As you can see, the distance reported by the meter doesn't always correspond to geographical distance. This is why feature engineering is important.

In the next part, you'll learn how to use these data features to create and train a machine learning model using Python.

## Next steps

In this article, you:

> [!div class="checklist"]
> + Modified a custom function to calculate trip distance
> + Saved the features using another custom function

> [!div class="nextstepaction"]
> [Python tutorial: Train and save a Python model using T-SQL](python-taxi-classification-train-model.md)