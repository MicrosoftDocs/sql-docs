public static void executeStatement(Connection con) {
   try {
      String SQL = "SELECT TOP 10 * FROM Person.Contact; " +
                   "SELECT TOP 20 * FROM Person.Contact";
      Statement stmt = con.createStatement();
      boolean results = stmt.execute(SQL);
      int rsCount = 0;

      //Loop through the available result sets.
     do {
        if(results) {
           ResultSet rs = stmt.getResultSet();
           rsCount++;

           //Show data from the result set.
           System.out.println("RESULT SET #" + rsCount);
           while (rs.next()) {
              System.out.println(rs.getString("LastName") + ", " + rs.getString("FirstName"));
           }
           rs.close();
        }
        System.out.println();
        results = stmt.getMoreResults();
        } while(results);
      stmt.close();
      }
   catch (Exception e) {
      e.printStackTrace();
   }
}