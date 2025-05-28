namespace AccountService.Application.Models
{
    public class PricePredictionResponse
    {
        public double Prediction { get; set; }
        public double[] Range { get; set; }
    }
} 