// Definir o endereço da API para as máquinas
const API = "http://localhost:5000/api/Maquina";

// Atribuir o evento de envio do formulário para a função salvarMáquina
document.getElementById("maquinaform").addEventListener("submit", salvarMaquina);

// Carregar as máquinas ao iniciar
carregarMaquinas();

function carregarMaquinas() {
    fetch(API)
        .then(res => res.json()) // Converter a resposta em formato JSON
        .then(data => {
            const tbody = document.querySelector("#tabelaMaquinas tbody");
            tbody.innerHTML = ""; // Limpar o conteúdo da tabela antes de adicionar os novos dados

            data.forEach(maquina => {
                tbody.innerHTML += `
                    <tr>
                        <td>${maquina.tipo}</td>
                        <td>${maquina.velocidade}</td>
                        <td>${maquina.hardDisk}</td>
                        <td>${maquina.placaRede}</td>
                        <td>${maquina.memoriaRam}</td>
                        <td>${maquina.fkUsuario}</td>
                        <td>
                            <button class="edit" onclick="editarMaquina(${maquina.id})">Editar</button>
                            <button class="delete" onclick="deletarMaquina(${maquina.id})">Deletar</button>
                        </td>
                    </tr>
                `;
            });
        });
}

function salvarMaquina(event) {
    event.preventDefault(); // Prevenir o envio padrão do formulário

    // Obter os dados do formulário
    const id = document.getElementById("id").value;
    const maquina = {
        id_maquina: parseInt(id || 0), // Se id não existir, atribui 0
        tipo: document.getElementById("tipo").value,
        velocidade: document.getElementById("velocidade").value,
        hardDisk: document.getElementById("harddisk").value,
        placaRede: document.getElementById("placarede").value,
        memoriaRam: document.getElementById("memoriaram").value,
        fkUsuario: document.getElementById("fk_usuario").value
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
        body: JSON.stringify(maquina) // Converter o objeto máquina para JSON
    })
        .then(res => res.json()) // Converter a resposta em JSON
        .then(() => {
            document.getElementById("maquinaform").reset(); // Resetar o formulário
            carregarMaquinas(); // Recarregar as máquinas
        });
}

function editarMaquina(id) {
    // Fazer uma requisição GET para pegar os dados da máquina pelo id
    fetch(`${API}/${id}`)
        .then(res => res.json())
        .then(maquina => {
            // Preencher os campos do formulário com os dados da máquina
            document.getElementById("id").value = maquina.id;
            document.getElementById("tipo").value = maquina.tipo;
            document.getElementById("velocidade").value = maquina.velocidade;
            document.getElementById("harddisk").value = maquina.hardDisk;
            document.getElementById("placarede").value = maquina.placaRede;
            document.getElementById("memoriaram").value = maquina.memoriaRam;
            document.getElementById("fk_usuario").value = maquina.fkUsuario;
        });
}

function deletarMaquina(id) {
    // Fazer a requisição DELETE para deletar a máquina
    fetch(`${API}/${id}`, { method: "DELETE" })
        .then(res => res.json())
        .then(() => carregarMaquinas()); // Recarregar a lista de máquinas após deletar
}
