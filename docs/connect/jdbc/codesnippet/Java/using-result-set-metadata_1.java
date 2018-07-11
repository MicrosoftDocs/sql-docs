public static void getResultSetMetaData(Connection con) {
   try {
      String SQL = "SELECT TOP 10 * FROM Person.Contact";
      Statement stmt = con.createStatement();
      ResultSet rs = stmt.executeQuery(SQL);
      ResultSetMetaData rsmd = rs.getMetaData();

      // Display the column name and type.
      int cols = rsmd.getColumnCount();
      for (int i = 1; i <= cols; i++) {
         System.out.println("NAME: " + rsmd.getColumnName(i) + " " + "TYPE: " + rsmd.getColumnTypeName(i));
      }
      rs.close();
      stmt.close();
   }
   catch (Exception e) {
      e.printStackTrace();
   }
}