class CustomModal {
    constructor() {
        this.modal = document.createElement('div'); //Cria o elemento div para o modal
        this.modal.className = 'modal-overlay';
        this.modal.innerHTML = `
            <div class="modal-content">
                <div class="modal-header">
                  <h3 class="modal-title"> Pessoas responsaveis</h3>
            <button class="modal-close">&times;</button>
        </div>
        <div class="modal-body">         
        </div>    
        <div class="modal-footer"></div>    
    </div>
        `;
        // appendChild é metodo que adiciona um elemento o filho a um elemento pai, no nosso caso        
        document.body.appendChild(this.modal);

        this.modal.querySelector('.modal-close').addEventListener('click', () => this.hide()); // Adcionando um evendto de clique ao botão de fechar do modal, qye chama o hide
    }
    show(title, body, buttons = []) { // metodo que mostra o modal
        this.modal.querySelector('.modal-title').textContent = title;  //define o titulo do modal
        this.modal.querySelector('.modal-body').innerHTML = body;  // define o conteudo do corpo do modal

        const footer = this.modal.querySelector('.modal-footer');
        footer.innerHTML = ''; // aqui to limpando o conteudo no rodape da pagina, da nova telinha que será criada

        buttons.forEach(btn => {
            const button = document.createElement('button'); // cria um novo botal
            button.className = `btn btn-${btn.type || 'secondary'}`;
            button.textContent = btn.text;
            button.addEventListener('click', () => { // vou colocar um enveto no click desse novo modal
                if (btn.handler) btn.handler();// se um manipulador de eventos for fornecido vou chamalo
                if (btn.close !== false) this.hide(); //  se close não for defindo como false, chama o medo hide para fechar o modal
            });
            footer.appendChild(button); //adicona o botão no rodape
        });
        this.modal.classList.add('active'); // Adiciona a classe active ao modal para tornalo visivel
        document.body.style.overflow = 'hidden'; // define o estilo de overflow do body como hidde para evitar rolagem de fundo
    }

    hide() { //Método de esconder o modal
        this.modal.classList.remove('active');// remove a classe active do modal para tornalo invisivel
        document.body.style.overflow = '';
    }

}