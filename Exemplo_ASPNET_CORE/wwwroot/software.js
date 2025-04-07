// Endereço da API para os Softwares
const API = "http://localhost:5000/api/Software";

// Atribuir o evento de envio do formulário
document.getElementById("softwareform").addEventListener("submit", salvarSoftware);

// Carregar a lista de softwares ao carregar a página
carregarSoftwares();

function carregarSoftwares() {
    fetch(API)
        .then(res => res.json()) // Converter a resposta em JSON
        .then(data => {
            const tbody = document.querySelector("#tabelaSoftwares tbody");
            tbody.innerHTML = ""; // Limpar o conteúdo da tabela antes de adicionar os novos dados

            data.forEach(software => {
                tbody.innerHTML += `
                    <tr>
                        <td>${software.produto}</td>
                        <td>${software.hardDisk}</td>
                        <td>${software.memoriaRam}</td>
                        <td>${software.fkMaquina}</td>
                        <td>
                            <button class="edit" onclick="editarSoftware(${software.id})">Editar</button>
                            <button class="delete" onclick="deletarSoftware(${software.id})">Deletar</button>
                        </td>
                    </tr>
                `;
            });
        });
}

function salvarSoftware(event) {
    event.preventDefault(); // Prevenir o envio padrão do formulário

    // Obter os dados do formulário
    const id = document.getElementById("id").value;
    const software = {
        id_software: parseInt(id || 0), // Se id não existir, atribui 0
        produto: document.getElementById("produto").value,
        hardDisk: document.getElementById("harddisk").value,
        memoriaRam: document.getElementById("memoriaram").value,
        fkMaquina: document.getElementById("fk_maquina").value
    };

    // Determinar o método (POST ou PUT) baseado na existência do id
    const metodo = id ? "PUT" : "POST";
    const url = id ? `${API}/${id}` : API;

    // Fazer a requisição HTTP
    fetch(url, {
        method: metodo,
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(software) // Converter o objeto software para JSON
    })
        .then(res => res.json()) // Converter a resposta em JSON
        .then(() => {
            document.getElementById("softwareform").reset(); // Resetar o formulário
            carregarSoftwares(); // Recarregar a lista de softwares
        });
}

function editarSoftware(id) {
    // Fazer uma requisição GET para pegar os dados do software pelo id
    fetch(`${API}/${id}`)
        .then(res => res.json())
        .then(software => {
            // Preencher os campos do formulário com os dados do software
            document.getElementById("id").value = software.id;
            document.getElementById("produto").value = software.produto;
            document.getElementById("harddisk").value = software.hardDisk;
            document.getElementById("memoriaram").value = software.memoriaRam;
            document.getElementById("fk_maquina").value = software.fkMaquina;
        });
}

function deletarSoftware(id) {
    // Fazer a requisição DELETE para deletar o software
    fetch(`${API}/${id}`, { method: "DELETE" })
        .then(res => res.json())
        .then(() => carregarSoftwares()); // Recarregar a lista de softwares após deletar
}
