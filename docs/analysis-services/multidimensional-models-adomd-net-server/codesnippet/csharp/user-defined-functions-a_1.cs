        public static Set FilterSet(Set set, String filterExpression)
        {
            Expression expr = new Expression(filterExpression);

            SetBuilder resultSetBuilder = new SetBuilder();

            foreach (Tuple tuple in set)
            {
                if ((bool)(expr.Calculate(tuple)))
                {
                    resultSetBuilder.Add(tuple);
                }
            }

            return resultSetBuilder.ToSet();
        }