namespace trabalhoav1.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime DataPedido { get; set; } = DateTime.Now;
        public decimal ValorTotal { get; set; }
        public string Status { get; set; } = "Pendente";
        public string EnderecoEntrega { get; set; } = string.Empty;
    }
}