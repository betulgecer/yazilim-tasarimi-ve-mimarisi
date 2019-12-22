# yazilim-tasarimi-ve-mimarisi
## Observe Tasarım Deseni
Observer tasarım deseni behavioral (davranışsal) grubuna aittir.  


Gözlemci ( Observer ) Tasarım Deseni Nedir?

Bir nesnenin durumlarında değişiklik olduğunda, bu değişikliklerden haberdar olmak isteyen diğer nesnelere haber verilmesi gerektiği durumlarda bu tasarım deseni kullanılır. Bu haber verilme işlemi sırasında, haber verilecek nesnelerin birbirlerine bağımlı olması istenmez. Yani kısaca dinleyici konumunda bulunan bir çok nesne, bir nesnenin durumunu sürekli gözlemler. Bir değişiklik sırasında gözlemcilere haber verilir.


Observer Tasarım Desenindeki Amaç:

Tasarlanmış olan sistem içerisinde, değişimini izlemek istediğimiz bir değer için kullanılır.


Observer Pattern Ne Zaman Kullanılır?  

-Bir nesneyi değiştirdiğinizde birden fazla nesnenin değişmesi gerekliyse ve kaç tane nesnenin değişeceğinden habersizseniz.  
-Eğer bir soyutlamanın birbirine bağlı iki görünümü varsa ve bu sınıfları birden fazla sınıfa ayırmak sonradan kullanımlarını kolaylaştırıyorsa.  
 -Eğer bir nesnenin diğer nesnelerin hangi nesneler olduğundan habersiz, onları haberdar etme ihtiyacı varsa ve bu nesneyle diğer nesneleri birbirine kuvvetli bağlarla bağlamak istemiyorsanız.


Observer Pattern Nerelerde Kullanılır?   

Örneğin bir alışveriş sitesinde bir ürüne indirim yapıldığında kullanıcılarınıza e-mail ile haber verilir iken bu kalıp kullanılabilir. Ya da en basitinden facebook da bir gruba üyesiniz grupta bildirimleri açtığınızda size (ve daha birçok kişiye) gelecek olan bildirim bu yapı ile olabilir.

Faydaları Nedir?

Loosely-coupled uygulamalar yapmayı sağlar. Subject ile Observer birbirleriyle loosely-coupled'tır.  
Bir nesnenin birden çok nesneyi otomatik olarak etkilemesini istiyorsak bu tasarım desenini kullanabiliriz. Örneğin, uygulamamızda A ve B kısımları olsun. A kısmında anlık sıcaklığın gösterildiğini varsayalım. B kısmı ise sıcaklık 20 derecenin altında olduğu zaman yeşil bir ışık göstersin. B kısmının sıcaklık değişikliklerine tepki göstermesi için kendisini A kısmının dinleyicisi(listener) olarak kaydetmesi gerekir. Kaydettikten sonra her bir sıcaklık değişimini izleyerek yeşil ışık gösterip göstermeyeceğini otomatik olarak kontrol eder.  


Observer Design Pattern’de;  
Subject: Takip edilecek olan nesnemiz.  
Observer: Abstract gözlemci sınıfımız. Soyutlamanın sebebi ise birden fazla gözlemci tarafından takip edilebilmesini sağlamak.  
ConcreteObserver: Gerçek takip eden nesnemiz.


![Imega of Class](https://github.com/betulgecer/yazilim-tasarimi-ve-mimarisi/blob/master/observerUML.png) 

Kodlar

Observer Interface
```java
public interface Observer {
    public void update(float temp, float humidity, float pressure);
}
```
Observer interface içerisinde sadece tek bir tane metod bulunması yeterlidir. Burada update() isimli metod kullanıldı.

Subject Interface
```java
public interface Subject {
    public void registerObserver(Observer o);
    public void removeObserver(Observer o);
    public void notifyObservers();
}
```
Subject interface içerisinde bulunan tüm metodlar zorunludur. Bunların dışında başka metodlar eklenebilir.

DisplayElement Interface
```java	
public interface DisplayElement {
    public void display();
}
```
Somut Subject Sınıfı
```java	
public class WeatherData implements Subject {
    private ArrayList observers;
    private float temperature;
    private float humidity;
    private float pressure;
           
    public WeatherData() {
        observers = new ArrayList();
    }
           
    public void registerObserver(Observer o) {
        observers.add(o);
    }
           
    public void removeObserver(Observer o) {
        int i = observers.indexOf(o);
        if (i >= 0) {
            observers.remove(i);
        }
    }
           
    public void notifyObservers() {
        for (int i = 0; i < observers.size(); i++) {
            Observer observer = (Observer)observers.get(i);
            observer.update(temperature, humidity, pressure);
        }
    }
           
    public void measurementsChanged() {
        notifyObservers();
    }
           
    public void setMeasurements(float temperature, float humidity, float pressure) {
        this.temperature = temperature;
        this.humidity = humidity;
        this.pressure = pressure;
        measurementsChanged();
    }
           
    public float getTemperature() {
        return temperature;
    }
           
    public float getHumidity() {
        return humidity;
    }
           
    public float getPressure() {
        return pressure;
    }
}
```  
WeatherData sınıfı Observer tasarım deseninde somut Subject sınıfını temsil eder. 


Somut Observer Sınıfları
```java
public class ForecastDisplay implements Observer, DisplayElement {
    private float currentPressure = 29.92f;  
    private float lastPressure;
    private WeatherData weatherData;
       
    public ForecastDisplay(WeatherData weatherData) {
        this.weatherData = weatherData;
        weatherData.registerObserver(this);
    }
       
    public void update(float temp, float humidity, float pressure) {
                lastPressure = currentPressure;
        currentPressure = pressure;
       
        display();
    }
       
    public void display() {
        System.out.print("Forecast: ");
        if (currentPressure > lastPressure) {
            System.out.println("Improving weather on the way!");
        } else if (currentPressure == lastPressure) {
            System.out.println("More of the same");
        } else if (currentPressure < lastPressure) {
            System.out.println("Watch out for cooler, rainy weather");
        }
    }
}
```
Somut Observer sınıfı. Görüldüğü gibi constructor parametre olarak somut Subject türünden bir nesne alıyor ve bu nesnenin registerObserver() metodunu kullanarak kendisini kaydediyor. update() metodunda ise Subject sınıfının yaptığı değişikliği alıyor ve işliyor. 

İkinci Somut Observer sınıfı.
```java
public class StatisticsDisplay implements Observer, DisplayElement {
    private float maxTemp = 0.0f;
    private float minTemp = 200;
    private float tempSum= 0.0f;
    private int numReadings;
    private WeatherData weatherData;
       
    public StatisticsDisplay(WeatherData weatherData) {
        this.weatherData = weatherData;
        weatherData.registerObserver(this);
    }
       
    public void update(float temp, float humidity, float pressure) {
        tempSum += temp;
        numReadings++;
       
        if (temp > maxTemp) {
            maxTemp = temp;
        }
        
        if (temp < minTemp) {
            minTemp = temp;
        }
       
        display();
    }
       
    public void display() {
        System.out.println("Avg/Max/Min temperature = " + (tempSum / numReadings)
            + "/" + maxTemp + "/" + minTemp);
    }
}
```

Test Sınıfı
```java
public class WeatherStation {
       
    public static void main(String[] args) {
        WeatherData weatherData = new WeatherData();
       
        StatisticsDisplay statisticsDisplay = new StatisticsDisplay(weatherData);
        ForecastDisplay forecastDisplay = new ForecastDisplay(weatherData);
       
        weatherData.setMeasurements(80, 65, 30.4f);
        weatherData.setMeasurements(82, 70, 29.2f);
        weatherData.setMeasurements(78, 90, 29.2f);
    }
}
```
Test sınıfı. Dikkat edersek, observer sınıflarından nesne yaratılırken, parametre olarak somut Subject sınıfı kullanıldı.


Genel olarak Behavioral (davranışsal) tasarım kalıplardan olan observer pattern sık kullanılır. One to many ilişkili birden fazla nesneden oluşur. Bir nesne değiştiği zaman, ona bağlı diğer nesnelerde otomatik olarak değişmektedir. Olay (event) bazlı değişimler olmaktadır.Gözlemci tasarım deseni gönderim tabanlı bildirim gerektiren tüm senaryolar için uygundur. Bu model, bir sağlayıcıyı ( Konu veya observableolarak da bilinir) ve sıfır, bir veya daha fazla gözlemcileri tanımlar. Gözlemcilerin sağlayıcıya kaydoldu ve önceden tanımlanmış bir koşul, olay veya durum değişikliği gerçekleştiğinde sağlayıcı, yöntemlerinden birini çağırarak tüm gözlemcilerin 'ı otomatik olarak bilgilendirir. Bu yöntem çağrısında sağlayıcı, güncel durum bilgilerini observers 'a de verebilir. 


 ##Iterator Tasarım Deseni
 
Iterator (tekrarlayıcı) tasarım deseni, behavior grubununa ait, nesne koleksiyonlarının (list,array,queue) 
elemanlarını belirlenen kurallara göre elde edilmesini düzenleyen tasarım desenidir. 


Iterator Tasarım Deseni Nedir?

Iterator tasarım deseni dizilerin, listlerin, queue’ların elemanlarını dolaşmak için kullanılan tasarım desenidir. En önemli özelliği dizi, queue ya da list olması bilinmeden elemanları üzerinde işlem yapabilmesidir. 


Iterator Tasarım Desenimdeki Amaç:
Desenin amacı nesne bütününü baştan sonra dolaşabilmektir.
Iterator tasarım deseni, bir listenin yapısının ve çalışma tarzının uygulamanın diğer kısımları ile olan 
bağlantılarını en aza indirmek için; listede yer alan nesnelerin, sırasıyla uygulamadan soyutlanması amacıyla kullanılır.


Iterator Tasarım Deseni Nerelerde Kullanılır?

Bir veri kümesini uygulamamızda diğer kısımlar ile olan bağlantısını en aza indirmek için;listede yer alan nesnelerin, 
sırasıyla uygulamadan soyutlanması amacıyla kullanılır.
Yani yapılacak olan işlemdeki nesneler ile olan tüm kontroller iterasyon deseninde gerçekleştirilir ve 
veri kümesi üzerinde bu iterasyonun kuralları çerçevesinde bir döngü söz konusu olur.
Sınıflar, bünyelerinde başka nesneleri barındırmak için değişik tipte listelere sahip olabilirler. 
Bu sınıfların nasıl implemente edildiği gizlemek ve sahip oldukları listeler üzerinde işlem yapmayı kolaylaştırmak
için Iterator tasarım şablonu kullanılır.

Iterator Tasarım Deseni Yapıları

Iterator tasarım deseninde 5 temel yapı bulunur.
-Iterator: Koleksiyon elemanları elde edilebilmesi için gerekli işlemleri tanımlar.
-Aggregate: Koleksiyon barındıran nesnelerin Iterator tipinden nesne olusturacağını belirten arayüzdür.
-Concrete Aggregate: Koleksiyon barındıran nesnedir. Aggregate arayüzünü uygular ve ilgili ConcreteIterator nesnesini oluşturur.
-ConcreteIterator: Aggregate yapısında ki koleksiyon elemanlarının elde edilmesini sağlayan metotları barındıran yani Iterator arayüzünü uygulayan gerçek iterator nesnesidir.
-Client: Bu yapıyı kullanarak koleksiyon içindeki elemanlara erişen yapıdır.


Kodlar




