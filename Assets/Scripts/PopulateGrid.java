
public class Main {
	
	static int[] types = new int[]{ 0, 1, 2, 3  };
	static final int N_TYPES = types.length;
	
	static int[] typesUsed = new int[ N_TYPES ];
	
	public static void main(String[] args)
	{
		int[][] grid = populateGrid();
		
		for( int i = 0; i < grid.length; i++ )
		{
			for( int j = 0; j < grid.length; j++ )
			{
				System.out.print( grid[i][j] );
			}
			System.out.println();
		}
		
		System.out.println("Times used:");
		for( int type = 0; type < N_TYPES; type++ )
			System.out.printf( "%d : used %d times\n", type, typesUsed[ type ]);
	}
	
	private static int[][] populateGrid()
	{
		int[][] grid = new int[ N_TYPES ][];
		for( int i = 0; i < grid.length; i++ )
			grid[i] = new int[ N_TYPES ];
		
		int[] checkArray;
		
		for( int row = 0; row < N_TYPES; row++ )
		{
			for( int column = 0; column < N_TYPES; /**/ )
			{
				int type = types[ ( int )( Math.random() * N_TYPES ) ];
				
				grid[ row ][ column ] = type;
				
				// increase the amount of times the number has been used
				typesUsed[ type ]++;
				
				// if the type has been used the max amount of times...
				if( typesUsed[ type ] > N_TYPES )
				{
					// ...don't advance
					typesUsed[ type ] = N_TYPES;
					continue;
				}
				
				// if you are not on the last row
				if( row != N_TYPES - 1 )
				{
					// also, if you are not on the last column
					if( column != N_TYPES - 1 )
					{
						// ...no need for checking, advance
						column++;
						continue;	
					}
					// if you are on the last column
					else
					{
						// check to see if there is a match along the horizontal axis
						checkArray = new int[ N_TYPES ];
						for( int i = 0; i < N_TYPES; i++ )
							checkArray[i] = grid[row][i];
						
						// if there is a match...
						if( match( checkArray ) )
						{
							// ...don't advance, try random type again
							continue;
						}
						// if there is no match...
						else
						{
							// ...advance
							column++;
							continue;
						}
					}
				}
				// if you are on the last row
				else
				{
					// check for matches vertically everytime
					checkArray = new int[ N_TYPES ];
					for( int i = 0; i < N_TYPES; i++ )
						checkArray[i] = grid[i][column];
					
					// if there is a match...
					if( match( checkArray ) )
					{
						// ...don't advance to next grid pos
						continue;
					}
					
					// if you are on the last column, you need to check horizontally as well
					if( column == N_TYPES - 1 )
					{
						checkArray = new int[ N_TYPES ];
						for( int i = 0; i < N_TYPES; i++ )
							checkArray[i] = grid[row][i];
						
						// if there is a match...
						if( match( checkArray ) )
						{
							// ...don't advance to next grid pos
							continue;
						}
						// if there is no match...
						else
						{
							// ...advance to the next grid pos
							column++;
							continue;
						}
					}
					// if you are on the last row, but not the last column, and there were no
					// vertical matches...
					else
					{
						// ...advance
						column++;
						continue;
					}
				}
			}
		}
		
		return grid;
	}
	
	private static boolean match(int ... chars)
	{
		int firstChar = chars[0];
		
		for( int i = 1; i < chars.length; i++ )
			if( chars[i] != firstChar )
				return false;
		
		return true;
	}

}


