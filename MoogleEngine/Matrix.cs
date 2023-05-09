namespace MoogleEngine;

public class Matrix
{
    public static double[,] MatrixSum(double[,] matrix1, double[,] matrix2)
    {
        double[,] matrix=new double[matrix1.GetLength(0),matrix1.GetLength(1)];

        if(matrix1.GetLength(0)==matrix2.GetLength(0) && matrix1.GetLength(0)==matrix2.GetLength(0))
        {
            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix1.GetLength(1); j++)
                {
                    matrix[i,j]=matrix1[i,j]+matrix2[i,j];
                }
            }
        }else
        {
            throw new Exception("The matrices cannot be sumed.");
        }
        return matrix;
    }

    public static double[,] MatrixRest(double[,] matrix1, double[,] matrix2)
    {
        double[,] matrix=new double[matrix1.GetLength(0),matrix1.GetLength(1)];

        if(matrix1.GetLength(0)==matrix2.GetLength(0) && matrix1.GetLength(0)==matrix2.GetLength(0))
        {
            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix1.GetLength(1); j++)
                {
                    matrix[i,j]=matrix1[i,j]-matrix2[i,j];
                }
            }
        }else
        {
            throw new Exception("The matrices cannot be sumed.");
        }
        return matrix;
    }

    public static double[,] MatrixMult(double[,] matrix1, double[,] matrix2)
    {
        double[,] matrix=new double[matrix1.GetLength(0),matrix2.GetLength(1)];

        if(matrix1.GetLength(0)!=matrix2.GetLength(1))
        {
            throw new Exception("The matrices cannot be multiplied.");
        }else
        {
            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix2.GetLength(1); j++)
                {
                    for (int k = 0; k < matrix1.GetLength(1); k++)
                    {
                        matrix[i,j]+=matrix1[i,k]*matrix2[k,j];
                    }
                }
            }
            return matrix;
        }
    }   

    public static double[,] TransMatrix(double[,] matrix1)
    {
        double[,] matrix=new double[matrix1.GetLength(1), matrix1.GetLength(0)];

        for (int i = 0; i < matrix1.GetLength(0); i++)
        {
            for (int j = 0; j < matrix1.GetLength(1); j++)
            {
                matrix[j,i]=matrix1[i,j];
            }
        }
        return matrix;
    } 

}