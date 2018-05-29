void Condition1()
{
	int a = int.Parse(Console.ReadLine());
	int b = int.Parse(Console.ReadLine());
	int c;
	if(a > b)
	{
		c = a + b;
		GetFunctionList();
		Block a = new Block();
	}
	else
	{
		c = b - a;
	}
	Console.Write(c);
		
}
