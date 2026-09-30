using Microsoft.AspNetCore.Mvc;
using trabalhoav1.Models;
using trabalhoav1.Repositories;

namespace trabalhoav1.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IRepository<Produto> _repository;

        public ProdutoController(IRepository<Produto> repository)
        {
            _repository = repository;
        }

        // Exibe a lista e, se receber editId, carrega o produto no formulário
        public async Task<IActionResult> Index(int? editId)
        {
            var produtos = await _repository.ObterTodosAsync();
            ViewBag.Produtos = produtos;

            if (editId.HasValue)
            {
                var produtoParaEditar = await _repository.ObterPorIdAsync(editId.Value);
                if (produtoParaEditar != null)
                {
                    return View(produtoParaEditar);
                }
            }

            return View(new Produto()); // Retorna modelo vazio para criação
        }

        [HttpPost]
        public async Task<IActionResult> Salvar(Produto produto)
        {
            if (ModelState.IsValid)
            {
                if (produto.Id == 0)
                {
                    await _repository.AdicionarAsync(produto);
                }
                else
                {
                    await _repository.AtualizarAsync(produto);
                }
                return RedirectToAction(nameof(Index));
            }

            // Se der erro de validação, recarrega a lista
            ViewBag.Produtos = await _repository.ObterTodosAsync();
            return View("Index", produto);
        }

        [HttpPost]
        public async Task<IActionResult> Deletar(int id)
        {
            await _repository.RemoverAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}