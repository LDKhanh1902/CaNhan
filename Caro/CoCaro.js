let check = true;
let Length = 30;
let quantity_square_height = Math.floor((screen.height - Math.round(screen.height*33/100)) / Length);
let quantity_square_width = Math.floor((screen.width - Math.round(screen.width * 10 / 100)) / Length);

const Caro = {
    init: {
        BanCo: () => {

            for (let i = 0; i < quantity_square_height; i++) {
                for (let j = 0; j < quantity_square_width; j++) {
                    let element = document.createElement('div');
                    element.style.position = 'absolute';
                    element.style.fontFamily = 'Forte';
                    element.id = `o_${i}_${j}`;
                    element.style.width = `${Length}px`;
                    element.style.height = `${Length}px`;
                    element.style.fontSize = `${Length}px`;
                    element.style.textAlign = 'center';
                    element.style.border = '1px solid gray';
                    element.style.marginLeft = `${j * Length}px`;
                    element.style.marginTop = `${i * Length}px`;
                    element.onclick = () => Caro.Processor.veX_O(element);
                    document.getElementById("chess-board").appendChild(element);
                }
            }
        },
        TieuDe: () => {
            let str = "Cờ Caro";

            for (let i = 0; i < str.length; i++) {
                let element = document.createElement('span');
                element.style.fontFamily = 'Forte';
                element.className = `tieude`;
                element.textContent = str[i];
                element.style.fontSize = '60px';
                document.getElementById('chess-title').appendChild(element);
            }
            setInterval(Caro.Processor.doiMauTieuDe, 1000)
        },
        ThanhDieuKhien: () => {
            let btnStart = document.createElement('button');
            btnStart.style.fontFamily = 'Forte';
            btnStart.style.fontSize = `${30}px`
            btnStart.style.width = `${100}px`;
            btnStart.style.height = `${100}px`;
            btnStart.textContent = 'Ván mới';
            btnStart.id = 'btnStart';
            btnStart.onclick = () => {
                Caro.Processor.gameStart(Caro.Processor.ArrayChess()),
                    Caro.Processor.chessboardClear(Caro.Processor.ArrayChess())
            };
            document.getElementById('chess-navbar').appendChild(btnStart);

            let btnBr = document.createElement('br');
            document.getElementById('chess-navbar').appendChild(btnBr);

            let btnRedo = document.createElement('button');
            btnRedo.style.fontFamily = 'Forte';
            btnRedo.style.fontSize = `${30}px`
            btnRedo.style.width = `${100}px`;
            btnRedo.style.height = `${100}px`;
            btnRedo.textContent = 'Lùi lại';
            btnRedo.id = 'btnStart';
            btnRedo.onclick = () => {
                Caro.Processor.Redo(Caro.Processor.ArrayRedo)
            };
            document.getElementById('chess-navbar').appendChild(btnRedo);
        },

    },
    Processor: {

        ArrayChess: () => {
            let array_chess = [];

            for (let i = 0; i < quantity_square_height; i++)
                array_chess.push([])

            for (let i = 0; i < quantity_square_width; i++)
                for (let j = 0; j < quantity_square_height; j++) {
                    array_chess[j][i] = document.getElementById(`o_${j}_${i}`);
                }
            return array_chess;
        },

        ArrayRedo: []
        ,
        Redo: (array) => {
            if (array.length == 0)
                return
            else {
                array[array.length - 1].textContent = '';
                array.pop();
                check = !check;
            }
        },

        veX_O: (element) => {

            if (element.textContent == '') {
                if (check) {
                    element.style.color = 'Red';
                    element.innerHTML = 'X';
                }
                else {
                    element.style.color = 'Blue';
                    element.innerHTML = 'O';
                }
                check = !check;
            }

            Caro.Processor.ArrayRedo.push(element);
            Caro.Processor.kiemTraDuongCheoChinh(Caro.Processor.ArrayChess());
            Caro.Processor.kiemTraDuongCheoPhu(Caro.Processor.ArrayChess());
            Caro.Processor.kiemTraCot(Caro.Processor.ArrayChess());
            Caro.Processor.kiemTraHang(Caro.Processor.ArrayChess());
        }
        ,

        kiemTraDuongCheoChinh: (array) => {
            for (let i = 0; i < array.length - 4; i++)
                for (let j = 0; j < array[0].length - 4; j++)
                    if (array[i][j].textContent != '' &&
                        array[i][j].textContent == array[i + 1][j + 1].textContent &&
                        array[i][j].textContent == array[i + 2][j + 2].textContent &&
                        array[i][j].textContent == array[i + 3][j + 3].textContent &&
                        array[i][j].textContent == array[i + 4][j + 4].textContent)
                        for (let n = 0; n < 5; n++) {
                            array[i + n][j + n].style.backgroundColor = 'orange';
                            if (n == 0)
                                Caro.Processor.gameOver(Caro.Processor.ArrayChess(), array[i][j]);
                        }
        }
        ,
        kiemTraHang: (array) => {
            for (let i = 0; i < array.length; i++)
                for (let j = 0; j < array[0].length - 4; j++)
                    if (array[i][j].textContent != '' &&
                        array[i][j].textContent == array[i][j + 1].textContent &&
                        array[i][j].textContent == array[i][j + 2].textContent &&
                        array[i][j].textContent == array[i][j + 3].textContent &&
                        array[i][j].textContent == array[i][j + 4].textContent)
                        for (let n = 0; n < 5; n++) {
                            array[i][j + n].style.backgroundColor = 'orange';
                            if (n == 0)
                                Caro.Processor.gameOver(Caro.Processor.ArrayChess(), array[i][j]);
                        }
        },

        kiemTraCot: (array) => {
            for (let i = 0; i < array.length - 4; i++)
                for (let j = 0; j < array[0].length; j++) {
                    if (array[i][j].textContent != '' &&
                        array[i][j].textContent == array[i + 1][j].textContent &&
                        array[i][j].textContent == array[i + 2][j].textContent &&
                        array[i][j].textContent == array[i + 3][j].textContent &&
                        array[i][j].textContent == array[i + 4][j].textContent)
                        for (let n = 0; n < 5; n++) {
                            array[i + n][j].style.backgroundColor = 'orange';
                            if (n == 0)
                                Caro.Processor.gameOver(Caro.Processor.ArrayChess(), array[i][j]);
                        }
                }
        },

        kiemTraDuongCheoPhu: (array) => {
            for (let i = 0; i < array.length - 4; i++)
                for (let j = array[0].length - 1; j > 3; j--) {
                    if (array[i][j].textContent != '' &&
                        array[i][j].textContent == array[i + 1][j - 1].textContent &&
                        array[i][j].textContent == array[i + 2][j - 2].textContent &&
                        array[i][j].textContent == array[i + 3][j - 3].textContent &&
                        array[i][j].textContent == array[i + 4][j - 4].textContent)
                        for (let n = 0; n < 5; n++) {
                            array[i + n][j - n].style.backgroundColor = 'orange';
                            if (n == 0)
                                Caro.Processor.gameOver(Caro.Processor.ArrayChess(), array[i][j]);
                        }
                }
        },

        gameStart: (array) => {
            for (let i = 0; i < array.length; i++)
                for (let j = 0; j < array[0].length; j++) {
                    array[i][j].onclick = () => { Caro.Processor.veX_O(array[i][j]) };
                }
            check = true;
            
        },

        gameOver: (array, element) => {
            Caro.Processor.thongBaoChienThang(element)
            for (let i = 0; i < array.length; i++)
                for (let j = 0; j < array[0].length; j++) {
                    array[i][j].onclick = () => { };
                }
        },

        chessboardClear: (array) => {
            for (let i = 0; i < array.length; i++)
                for (let j = 0; j < array[0].length; j++) {
                    array[i][j].textContent = '';
                    array[i][j].style.backgroundColor = 'white';
                }
        }
        ,
        doiMauTieuDe: () => {
            let arraycolor = ['red', 'blue', 'yellow', 'green', 'violet', 'orange'];
            let title = document.getElementsByClassName(`tieude`);

            for (let i = 0; i < title.length; i++) {
                if (title[i].textContent != ' ') {
                    let color = arraycolor[Math.floor(Math.random() * arraycolor.length)];
                    title[i].style.color = color;
                    title[i].style.position = 'relative';
                    arraycolor.splice(arraycolor.indexOf(color), 1);
                }
            }
        },
        thongBaoChienThang: (element) => {
            let messenge = document.createElement('div');
            messenge.style.display = 'grid';
            messenge.id = 'messenge';
            messenge.textContent = `${element.style.color} win`;
            messenge.style.color = `${element.style.color}`
            messenge.style.position = 'absolute';
            messenge.style.fontFamily = 'Forte';
            messenge.style.backgroundColor = 'yellow';
            messenge.style.fontSize = '150px';
            messenge.style.textAlign = 'center';
            messenge.style.width = `600px`;
            messenge.style.height = `400px`;
            messenge.style.transform = 'translate(50%,25%)';
            document.body.appendChild(messenge);

            let btnOK = document.createElement('button');
            btnOK.id = 'btnOK';
            btnOK.textContent = "OK";
            btnOK.style.width = `100px`;
            btnOK.style.height = `100px`;
            btnOK.style.position = 'absolute';
            btnOK.style.justifySelf = 'center';
            btnOK.style.fontFamily = 'Forte';
            btnOK.style.bottom = '0px';
            btnOK.onclick = () => { messenge.remove(); }
            document.getElementById('messenge').appendChild(btnOK);
        }
    }
}

Caro.init.BanCo()
Caro.init.TieuDe()
Caro.init.ThanhDieuKhien()


