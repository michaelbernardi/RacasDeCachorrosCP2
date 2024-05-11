using Microsoft.AspNetCore.Mvc;
using RacasDeCachorros.Models;

namespace Cachorro.Controllers;

public class CachorroController : Controller
{
    //Lista de cachorros para simular o banco de dados
    private static List<Cachorro> _lista = new List<Cachorro>();
    private static int _id = 0; //Controla o ID

    [HttpGet] //Abrir o formulário com os dados preenchidos
    public IActionResult PesquisaNome(string searchString)
    {
        if (string.IsNullOrEmpty(searchString))
        {
            // Se a string de pesquisa estiver vazia, redireciona para a lista de cachorros
            return RedirectToAction("Index");
        }

        // Procura cachorros que correspondam ao termo de pesquisa (insensível a maiúsculas e minúsculas)
        var cachorros = _lista.Where(c => c.Nome!.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();

        if (cachorros.Count == 0)
        {
            // Caso nenhum cachorro for encontrado
            TempData["msg"] = "Nenhum cachorro Encontrado!!";
            return RedirectToAction("Index");
        }

        // Se os cachorros forem encontrados, envie-os para a visualização de índice
        return View("Index", cachorros);
    }

    // Create
    [HttpGet] //Abrir a página com o formulário HTML
    public IActionResult Cadastrar()
    {
        ViewBag.teste = "Teste";
        return View();
    }

    [HttpPost]
    public IActionResult Cadastrar(Cachorro cachorro)
    {
        //Setar o código do cachorro
        cachorro.CachorroId = ++_id;

        //Adicionar o cachorro na lista
        _lista.Add(cachorro);

        //Mandar uma mensagem de sucesso para a view
        TempData["msg"] = "Cachorro cadastrado com sucesso!";

        //Redireciona para o método Cadastrar
        return RedirectToAction("Cadastrar");
    }

    // Read
    public IActionResult Index()
    {
        //Enviar a lista de cachorro para a view
        return View(_lista);
    }

    // Update
    [HttpGet] //Abrir o formulário com os dados preenchidos
    public IActionResult Editar(int id)
    {
        //Recuperar a posição do cachorro na lista através do id
        var index = _lista.FindIndex(c => c.CachorroId == id);
        //Recuperar o cachorro através do ID
        var cachorro = _lista[index];
        //Enviar o cachorro para a view
        return View(cachorro);
    }

    [HttpPost]
    public IActionResult Editar(Cachorro cachorro)
    {
        //Atualizar o cachorro na lista
        var index = _lista.FindIndex(c => c.achorroId == cachorro.CachorroId);
        //Substitui o objeto na posição do cachorro antigo
        _lista[index] = cachorro;
        //Mensagem de sucesso
        TempData["msg"] = "cachorro atualizado com sucesso!";
        //Redirect para a listagem/editar
        return RedirectToAction("editar");
    }

    // Remove 
    [HttpPost]
    public IActionResult Remover(int id)
    {
        //Remover o cachorro da lista
        _lista.RemoveAt(_lista.FindIndex(c => c.CachorroId == id));
        //Mensagem de sucesso
        TempData["msg"] = "cachorro removido com sucesso!";
        //Redirecionar para a listagem
        return RedirectToAction("Index");
    }
}
