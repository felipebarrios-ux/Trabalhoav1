using Microsoft.AspNetCore.Mvc;
using trabalhoav1.Models;
using trabalhoav1.Repositories;

namespace trabalhoav1.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IRepository<Pedido> _repository;

        public PedidoController(IRepository<Pedido> repository)
        {
            _repository = repository;
        }

        // Exibe a lista e, se receber editId, carrega o pedido no formulário
        public async Task<IActionResult> Index(int? editId)
        {
            var pedidos = await _repository.ObterTodosAsync();
            ViewBag.Pedidos = pedidos;

            if (editId.HasValue)
            {
                var pedidoParaEditar = await _repository.ObterPorIdAsync(editId.Value);
                if (pedidoParaEditar != null)
                {
                    return View(pedidoParaEditar);
                }
            }

            return View(new Pedido()); // Retorna modelo vazio para criação
        }

        [HttpPost]
        public async Task<IActionResult> Salvar(Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                if (pedido.Id == 0)
                {
                    await _repository.AdicionarAsync(pedido);
                }
                else
                {
                    await _repository.AtualizarAsync(pedido);
                }
                return RedirectToAction(nameof(Index));
            }

            // Se der erro de validação, recarrega a lista
            ViewBag.Pedidos = await _repository.ObterTodosAsync();
            return View("Index", pedido);
        }

        [HttpPost]
        public async Task<IActionResult> Deletar(int id)
        {
            await _repository.RemoverAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}