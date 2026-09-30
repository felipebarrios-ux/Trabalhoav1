using Microsoft.AspNetCore.Mvc;
using trabalhoav1.Models;
using trabalhoav1.Repositories;

namespace trabalhoav1.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IRepository<Cliente> _repository;

        public ClienteController(IRepository<Cliente> repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index(int? editId)
        {
            var clientes = await _repository.ObterTodosAsync();
            ViewBag.Clientes = clientes;

            if (editId.HasValue)
            {
                var clienteParaEditar = await _repository.ObterPorIdAsync(editId.Value);
                return View(clienteParaEditar);
            }

            return View(new Cliente());
        }

        [HttpPost]
        public async Task<IActionResult> Salvar(Cliente cliente)
        {
            if (cliente.Id == 0)
                await _repository.AdicionarAsync(cliente);
            else
                await _repository.AtualizarAsync(cliente);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Excluir(int id)
        {
            await _repository.RemoverAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}